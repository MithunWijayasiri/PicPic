(() => {
  "use strict";
  const $ = (s, r = document) => r.querySelector(s);
  const $$ = (s, r = document) => [...r.querySelectorAll(s)];

  /* Nav state and mobile menu */
  const nav = $("#nav");
  const syncNav = () => nav.classList.toggle("scrolled", window.scrollY > 8);
  syncNav();
  window.addEventListener("scroll", syncNav, { passive: true });

  const menuBtn = $("#menuBtn");
  const mobileMenu = $("#mobileMenu");
  const setMenu = (open) => {
    mobileMenu.hidden = !open;
    menuBtn.setAttribute("aria-expanded", String(open));
    menuBtn.setAttribute("aria-label", open ? "Close menu" : "Open menu");
  };
  menuBtn.addEventListener("click", () => setMenu(mobileMenu.hidden));
  $$("a", mobileMenu).forEach((a) => a.addEventListener("click", () => setMenu(false)));
  document.addEventListener("keydown", (e) => {
    if (e.key === "Escape" && !mobileMenu.hidden) setMenu(false);
  });

  const navLinks = $$("[data-nav]");
  if ("IntersectionObserver" in window) {
    const sectionIO = new IntersectionObserver((entries) => {
      entries.forEach((en) => {
        if (!en.isIntersecting) return;
        navLinks.forEach((a) => a.classList.toggle("active", a.getAttribute("href") === "#" + en.target.id));
      });
    }, { rootMargin: "-40% 0px -55% 0px" });
    ["features", "formats", "plugins", "release", "beta"].forEach((id) => {
      const el = document.getElementById(id);
      if (el) sectionIO.observe(el);
    });
  }

  /* Hero drift: damped follow of scroll on stage container */
  const heroStage = $("#heroStage") || $("#heroImg");
  const reduced = window.matchMedia("(prefers-reduced-motion: reduce)").matches;
  if (heroStage && !reduced) {
    let target = 0, current = 0, ticking = false;
    const frame = () => {
      current += (target - current) * 0.12;
      heroStage.style.transform = `translateY(${(current * -0.06).toFixed(2)}px)`;
      if (Math.abs(target - current) > 0.05) {
        requestAnimationFrame(frame);
      } else {
        ticking = false;
      }
    };
    window.addEventListener("scroll", () => {
      target = window.scrollY;
      if (!ticking) {
        ticking = true;
        requestAnimationFrame(frame);
      }
    }, { passive: true });
  }

  /* Interactive image viewer demo */
  const demoImages = [
    {
      src: "assets/demo-neon-dusk.jpg",
      alt: "Sample image: mountain lake at dusk with vibrant neon aurora glow",
      title: "PicPic — demo-neon-dusk.jpg (1536 × 1024)",
      info: "1536 × 1024 • 2.4 MB • JPEG"
    },
    {
      src: "assets/demo-midnight-avenue.jpg",
      alt: "Sample image: illuminated rain-slicked city avenue at midnight",
      title: "PicPic — demo-midnight-avenue.jpg (1024 × 1536)",
      info: "1024 × 1536 • 1.9 MB • JPEG"
    },
    {
      src: "assets/demo-prism-study.jpg",
      alt: "Sample image: geometric optical prism refracting vibrant spectrums",
      title: "PicPic — demo-prism-study.jpg (1024 × 1024)",
      info: "1024 × 1024 • 1.2 MB • JPEG"
    }
  ];

  let currentIndex = 0;
  let rotationDeg = 0;
  let zoomScale = 1.0;
  let slideshowTimer = null;

  const heroImg = $("#heroImg");
  const demoTitle = $("#demoTitle");
  const demoInfo = $("#demoInfo");
  const hudSlideshow = $("#hudSlideshow");

  const applyTransform = () => {
    if (!heroImg) return;
    heroImg.style.transform = `rotate(${rotationDeg}deg) scale(${zoomScale})`;
    if (demoInfo) {
      const base = demoImages[currentIndex].info;
      const zoomText = `${Math.round(zoomScale * 100)}%`;
      const rotText = rotationDeg ? ` • ${rotationDeg}°` : "";
      demoInfo.textContent = `${base} • ${zoomText}${rotText}`;
    }
  };

  const showImage = (index) => {
    if (!heroImg) return;
    currentIndex = (index + demoImages.length) % demoImages.length;
    const item = demoImages[currentIndex];
    heroImg.style.opacity = "0.6";
    heroImg.src = item.src;
    heroImg.alt = item.alt;
    if (demoTitle) demoTitle.textContent = item.title;
    rotationDeg = 0;
    zoomScale = 1.0;
    applyTransform();
    setTimeout(() => {
      heroImg.style.opacity = "1";
    }, 100);
  };

  const nextImage = () => showImage(currentIndex + 1);
  const prevImage = () => showImage(currentIndex - 1);

  const rotateImage = () => {
    rotationDeg = (rotationDeg + 90) % 360;
    applyTransform();
  };

  const zoomImage = (delta) => {
    zoomScale = Math.min(2.0, Math.max(0.6, Math.round((zoomScale + delta) * 10) / 10));
    applyTransform();
  };

  const stopSlideshow = () => {
    if (!slideshowTimer) return;
    clearInterval(slideshowTimer);
    slideshowTimer = null;
    if (hudSlideshow) {
      hudSlideshow.classList.remove("active");
      hudSlideshow.title = "Start slideshow (Space)";
      hudSlideshow.setAttribute("aria-label", "Start slideshow (Space)");
      const playIcon = $(".icon-play", hudSlideshow);
      const pauseIcon = $(".icon-pause", hudSlideshow);
      const label = $(".win-hud-label", hudSlideshow);
      if (playIcon) playIcon.removeAttribute("hidden");
      if (pauseIcon) pauseIcon.setAttribute("hidden", "");
      if (label) label.textContent = "Play";
    }
  };

  const toggleSlideshow = () => {
    if (slideshowTimer) {
      stopSlideshow();
    } else {
      nextImage();
      slideshowTimer = setInterval(nextImage, 2600);
      if (hudSlideshow) {
        hudSlideshow.classList.add("active");
        hudSlideshow.title = "Pause slideshow (Space)";
        hudSlideshow.setAttribute("aria-label", "Pause slideshow (Space)");
        const playIcon = $(".icon-play", hudSlideshow);
        const pauseIcon = $(".icon-pause", hudSlideshow);
        const label = $(".win-hud-label", hudSlideshow);
        if (playIcon) playIcon.setAttribute("hidden", "");
        if (pauseIcon) pauseIcon.removeAttribute("hidden");
        if (label) label.textContent = "Pause";
      }
    }
  };

  $("#demoPrev")?.addEventListener("click", () => { stopSlideshow(); prevImage(); });
  $("#demoNext")?.addEventListener("click", () => { stopSlideshow(); nextImage(); });
  $("#hudPrev")?.addEventListener("click", () => { stopSlideshow(); prevImage(); });
  $("#hudNext")?.addEventListener("click", () => { stopSlideshow(); nextImage(); });
  $("#hudRotate")?.addEventListener("click", rotateImage);
  $("#hudZoomIn")?.addEventListener("click", () => zoomImage(0.2));
  $("#hudZoomOut")?.addEventListener("click", () => zoomImage(-0.2));
  $("#hudSlideshow")?.addEventListener("click", toggleSlideshow);

  const winCanvas = $(".win-canvas");
  if (winCanvas) {
    winCanvas.addEventListener("keydown", (e) => {
      if (e.ctrlKey || e.metaKey || e.altKey) return;

      if (e.key === "ArrowLeft") {
        e.preventDefault();
        stopSlideshow();
        prevImage();
      } else if (e.key === "ArrowRight") {
        e.preventDefault();
        stopSlideshow();
        nextImage();
      } else if (e.key === "r" || e.key === "R") {
        e.preventDefault();
        rotateImage();
      } else if (e.key === "+" || e.key === "=") {
        e.preventDefault();
        zoomImage(0.2);
      } else if (e.key === "-" || e.key === "_") {
        e.preventDefault();
        zoomImage(-0.2);
      } else if (e.key === " ") {
        if (e.target !== winCanvas) return;
        e.preventDefault();
        toggleSlideshow();
      }
    });
  }
})();
