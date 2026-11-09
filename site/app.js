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
    ["features", "formats", "plugins", "beta"].forEach((id) => sectionIO.observe(document.getElementById(id)));
  }

  /* Hero drift: damped follow of scroll, settled ease-out */
  const heroImg = $("#heroImg");
  const reduced = window.matchMedia("(prefers-reduced-motion: reduce)").matches;
  if (heroImg && !reduced) {
    let target = 0, current = 0, ticking = false;
    const frame = () => {
      current += (target - current) * 0.12;
      heroImg.style.transform = `translateY(${(current * -0.08).toFixed(2)}px)`;
      if (Math.abs(target - current) > 0.05 || window.scrollY < window.innerHeight * 1.2) {
        ticking = true;
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
})();
