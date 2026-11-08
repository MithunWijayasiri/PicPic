(() => {
  "use strict";
  const $ = (s, r = document) => r.querySelector(s);
  const $$ = (s, r = document) => [...r.querySelectorAll(s)];

  /* ============ Nav ============ */
  const nav = $("#nav");
  const onNavScroll = () => nav.classList.toggle("scrolled", window.scrollY > 8);
  onNavScroll();
  window.addEventListener("scroll", onNavScroll, { passive: true });

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
    document.documentElement.classList.add("reveal-armed");
    const sectionIO = new IntersectionObserver((entries) => {
      entries.forEach((en) => {
        if (!en.isIntersecting) return;
        navLinks.forEach((a) => a.classList.toggle("active", a.getAttribute("href") === "#" + en.target.id));
      });
    }, { rootMargin: "-40% 0px -55% 0px" });
    ["features", "formats", "plugins", "beta"].forEach((id) => sectionIO.observe(document.getElementById(id)));

    /* ============ Reveal on scroll ============ */
    const revealIO = new IntersectionObserver((entries) => {
      entries.forEach((en) => {
        if (en.isIntersecting) {
          en.target.classList.add("revealed");
          revealIO.unobserve(en.target);
        }
      });
    }, { threshold: 0.12 });
    $$("[data-reveal]").forEach((el) => revealIO.observe(el));
  }

  /* ============ Floating chip parallax ============ */
  const floats = $$("[data-parallax]");
  let floatTick = false;
  const parallax = () => {
    const y = window.scrollY;
    floats.forEach((el) => {
      el.style.transform = `translateY(${y * parseFloat(el.dataset.parallax) * -1}px)`;
    });
    floatTick = false;
  };
  window.addEventListener("scroll", () => {
    if (!floatTick && window.scrollY < window.innerHeight * 1.2) {
      floatTick = true;
      requestAnimationFrame(parallax);
    }
  }, { passive: true });

  /* ============ Feature-card micro demos ============ */
  const miniImg = $("#miniImg");
  let miniRot = 0, miniFlip = 1;
  const miniApply = () => { miniImg.style.transform = `rotate(${miniRot}deg) scaleX(${miniFlip})`; };
  $("#miniRotate").addEventListener("click", () => { miniRot = (miniRot + 90) % 360; miniApply(); });
  $("#miniFlip").addEventListener("click", () => { miniFlip *= -1; miniApply(); });

  /* ============ Plugin manager demo ============ */
  const pmPool = [
    { id: "ocr", file: "ocr.dll", desc: "Extract text from any image", color: "#FF3D6E" },
    { id: "imgur", file: "imgur-upload.dll", desc: "Upload and share in one click", color: "#22D3FF" },
    { id: "picker", file: "color-picker.dll", desc: "Copy any pixel's color", color: "#FFE14D" },
    { id: "exif", file: "exif-viewer.dll", desc: "Read camera metadata", color: "#7EC77F" },
  ];
  const pmPanel = $("#pluginPanel");
  const pmList = $("#pmList");
  const pmDrop = $("#pmDrop");
  const pmCount = $("#pmCount");
  const pmStatus = $("#pmStatus");
  let pmInstalled = new Map();

  const pmSay = (msg, ok) => {
    pmStatus.textContent = msg;
    pmStatus.dataset.tone = ok ? "ok" : "";
  };

  function pmRender(enterId) {
    pmList.innerHTML = pmInstalled.size === 0
      ? '<li class="pm-empty">No plugins yet — drop one in.</li>'
      : pmPool.filter(p => pmInstalled.has(p.id)).map(p => `
        <li class="pm-row${pmInstalled.get(p.id) ? "" : " off"}${p.id === enterId ? " entering" : ""}" data-id="${p.id}">
          <span class="pm-dot" style="--pc:${p.color}" aria-hidden="true"></span>
          <span class="pm-info"><b>${p.file}</b><small>${p.desc}</small></span>
          <button type="button" class="pm-toggle" role="switch" aria-checked="${pmInstalled.get(p.id)}" aria-label="Enable ${p.file}"><span class="pm-knob"></span></button>
          <button type="button" class="pm-remove" aria-label="Remove ${p.file}"><svg viewBox="0 0 12 12"><path d="M2 2l8 8M10 2l-8 8"/></svg></button>
        </li>`).join("");
    pmCount.textContent = `${pmInstalled.size} installed`;
    const next = pmPool.find(p => !pmInstalled.has(p.id));
    pmDrop.innerHTML = next
      ? `<button type="button" class="pm-chip" draggable="true" data-id="${next.id}" style="--pc:${next.color}" aria-label="Install ${next.file} — click, or drag into the plugins panel">
          <svg viewBox="0 0 20 20"><path d="M5 2h7l4 4v12H5ZM12 2v4h4M10 9v6M7.5 12.5 10 15l2.5-2.5"/></svg>
          <span>${next.file}</span><span class="pm-chip-hint">drop to install</span>
        </button>`
      : `<div class="pm-alldone"><span>Sample pack installed.</span><button type="button" class="pm-reset" id="pmReset">Reset demo</button></div>`;
  }

  function pmInstall(id) {
    const p = pmPool.find(p => p.id === id);
    if (!p || pmInstalled.has(id)) return;
    pmInstalled.set(id, true);
    pmRender(id);
    pmSay(`${p.file} installed`, true);
  }

  function pmReset() {
    pmInstalled = new Map([["ocr", true], ["imgur", true]]);
    pmRender();
    pmSay("Demo reset");
  }
  pmReset();
  pmStatus.textContent = "";

  pmList.addEventListener("click", (e) => {
    const row = e.target.closest(".pm-row");
    if (!row) return;
    const p = pmPool.find(p => p.id === row.dataset.id);
    if (e.target.closest(".pm-toggle")) {
      const on = !pmInstalled.get(p.id);
      pmInstalled.set(p.id, on);
      row.classList.toggle("off", !on);
      $(".pm-toggle", row).setAttribute("aria-checked", String(on));
      pmSay(`${p.file} ${on ? "enabled" : "disabled"}`, on);
    } else if (e.target.closest(".pm-remove")) {
      pmInstalled.delete(p.id);
      pmRender();
      pmSay(`${p.file} removed`);
    }
  });

  pmDrop.addEventListener("click", (e) => {
    const chip = e.target.closest(".pm-chip");
    if (chip) pmInstall(chip.dataset.id);
    else if (e.target.closest(".pm-reset")) pmReset();
  });
  pmDrop.addEventListener("dragstart", (e) => {
    const chip = e.target.closest(".pm-chip");
    if (!chip) return;
    e.dataTransfer.setData("text/plain", chip.dataset.id);
    e.dataTransfer.effectAllowed = "copy";
  });
  pmPanel.addEventListener("dragover", (e) => {
    if (!e.dataTransfer.types.includes("text/plain")) return;
    e.preventDefault();
    e.dataTransfer.dropEffect = "copy";
    pmPanel.classList.add("drop-hot");
  });
  pmPanel.addEventListener("dragleave", (e) => {
    if (!pmPanel.contains(e.relatedTarget)) pmPanel.classList.remove("drop-hot");
  });
  pmPanel.addEventListener("drop", (e) => {
    e.preventDefault();
    pmPanel.classList.remove("drop-hot");
    pmInstall(e.dataTransfer.getData("text/plain"));
  });

  /* ============ PicPic viewer preview ============ */
  const files = [
    { src: "assets/demo-neon-dusk.jpg", name: "neon-dusk.jpg", dim: "1536 × 1024", size: "273 KB", fmt: "JPEG",
      alt: "Sample image: mountain lake at dusk with pink and cyan aurora light" },
    { src: "assets/demo-prism-study.jpg", name: "prism-study.jpg", dim: "1024 × 1024", size: "101 KB", fmt: "JPEG",
      alt: "Sample image: light refracting through glass prisms into pink, cyan and amber ribbons" },
    { src: "assets/demo-midnight-avenue.jpg", name: "midnight-avenue.jpg", dim: "1024 × 1536", size: "299 KB", fmt: "JPEG",
      alt: "Sample image: rain-soaked neon city street at night" },
    { src: "assets/picpic-mark.svg", name: "picpic-mark.svg", dim: "704 × 704", size: "514 B", fmt: "SVG",
      alt: "Sample vector image: the PicPic logo mark" },
  ];

  const viewer = $("#viewer");
  const slot = $("#viewerSlot");
  const stage = $("#vStage");
  const img = $("#vImg");
  const vFile = $("#vFile"), vIndex = $("#vIndex"), vFmt = $("#vFmt"), vZoom = $("#vZoom");
  const vInfo = $("#vInfo"), vProgress = $("#vProgress");
  const iName = $("#iName"), iDim = $("#iDim"), iSize = $("#iSize"), iFmt = $("#iFmt"), iZoom = $("#iZoom");
  const placeholder = $("#pipPlaceholder");
  const btn = (a) => $(`[data-action="${a}"]`, viewer);

  let idx = 0, rot = 0, flipH = 1, flipV = 1, zoom = 1;
  let pan = { x: 0, y: 0 };
  let playing = false, timer = null;
  let userChecker = false, pip = false;

  const zoomLabel = () => (zoom === 1 ? "Fit" : Math.round(zoom * 100) + "%");

  function apply() {
    img.style.transform = `translate(${pan.x}px, ${pan.y}px) scale(${zoom * flipH}, ${zoom * flipV}) rotate(${rot}deg)`;
    stage.classList.toggle("zoomed", zoom > 1);
    vZoom.textContent = zoomLabel();
    iZoom.textContent = zoomLabel();
  }

  function updateMeta() {
    const f = files[idx];
    vFile.textContent = f.name;
    vIndex.textContent = `${idx + 1} / ${files.length}`;
    vFmt.textContent = f.fmt;
    iName.textContent = f.name;
    iDim.textContent = f.dim;
    iSize.textContent = f.size;
    iFmt.textContent = f.fmt;
    img.alt = f.alt;
    stage.classList.toggle("checker", userChecker || f.fmt === "SVG");
  }

  function show(i) {
    idx = ((i % files.length) + files.length) % files.length;
    rot = 0; flipH = 1; flipV = 1; zoom = 1; pan = { x: 0, y: 0 };
    img.classList.add("switching");
    const pre = new Image();
    pre.onload = () => {
      img.src = files[idx].src;
      updateMeta();
      apply();
      requestAnimationFrame(() => img.classList.remove("switching"));
    };
    pre.src = files[idx].src;
    [idx + 1, idx - 1].forEach((n) => {
      const p = new Image();
      p.src = files[((n % files.length) + files.length) % files.length].src;
    });
  }

  function tickProgress() {
    vProgress.classList.remove("run");
    void vProgress.offsetWidth;
    if (playing) vProgress.classList.add("run");
  }

  function resetTimer() {
    clearInterval(timer);
    if (playing) timer = setInterval(() => { show(idx + 1); tickProgress(); }, 2600);
    tickProgress();
  }

  function navImg(d) { show(idx + d); if (playing) resetTimer(); }
  function zoomBy(f) { zoom = Math.min(6, Math.max(0.25, zoom * f)); if (zoom < 1.02 && zoom > 0.98) zoom = 1; apply(); }
  function fit() { zoom = 1; pan = { x: 0, y: 0 }; apply(); }

  function setPlaying(on) {
    playing = on;
    const b = btn("slideshow");
    b.classList.toggle("playing", on);
    b.classList.toggle("active", on);
    b.setAttribute("aria-pressed", String(on));
    b.setAttribute("aria-label", on ? "Pause slideshow" : "Play slideshow");
    b.dataset.tip = on ? "Pause slideshow" : "Play slideshow";
    resetTimer();
  }

  function setPip(on) {
    pip = on;
    const b = btn("pip");
    b.classList.toggle("active", on);
    b.setAttribute("aria-pressed", String(on));
    b.setAttribute("aria-label", on ? "Restore from picture-in-picture" : "Picture-in-picture");
    b.dataset.tip = on ? "Restore viewer" : "Picture-in-picture";
    if (on) {
      slot.style.minHeight = viewer.offsetHeight + "px";
      viewer.classList.add("pip");
      placeholder.hidden = false;
    } else {
      viewer.classList.remove("pip");
      placeholder.hidden = true;
      slot.style.minHeight = "";
    }
  }

  const actions = {
    prev: () => navImg(-1),
    next: () => navImg(1),
    "rotate-ccw": () => { rot = (rot + 270) % 360; apply(); },
    "rotate-cw": () => { rot = (rot + 90) % 360; apply(); },
    "flip-h": () => { flipH *= -1; apply(); },
    "flip-v": () => { flipV *= -1; apply(); },
    "zoom-out": () => zoomBy(1 / 1.25),
    "zoom-in": () => zoomBy(1.25),
    fit,
    slideshow: () => setPlaying(!playing),
    checker: () => {
      userChecker = !userChecker;
      btn("checker").classList.toggle("active", userChecker);
      btn("checker").setAttribute("aria-pressed", String(userChecker));
      updateMeta();
    },
    info: () => {
      vInfo.hidden = !vInfo.hidden;
      btn("info").classList.toggle("active", !vInfo.hidden);
      btn("info").setAttribute("aria-pressed", String(!vInfo.hidden));
    },
    theme: () => {
      const light = viewer.classList.toggle("light");
      btn("theme").classList.toggle("lit", light);
      btn("theme").setAttribute("aria-pressed", String(light));
      btn("theme").setAttribute("aria-label", light ? "Toggle dark mode" : "Toggle light mode");
      btn("theme").dataset.tip = light ? "Toggle dark mode" : "Toggle light mode";
    },
    pip: () => setPip(!pip),
  };

  $$("[data-action]", viewer).forEach((b) => b.addEventListener("click", () => actions[b.dataset.action]()));
  $("#pipRestore").addEventListener("click", () => setPip(false));

  viewer.addEventListener("keydown", (e) => {
    if (e.target.closest("button")) return;
    const map = {
      ArrowLeft: () => navImg(-1), ArrowRight: () => navImg(1),
      "+": () => zoomBy(1.25), "=": () => zoomBy(1.25), "-": () => zoomBy(1 / 1.25),
      "0": fit, r: () => actions["rotate-cw"](), f: () => actions["flip-h"](),
      i: () => actions.info(), " ": () => setPlaying(!playing),
    };
    if (e.key === "Escape") {
      if (pip) setPip(false);
      else if (!vInfo.hidden) actions.info();
      return;
    }
    const fn = map[e.key];
    if (fn) { e.preventDefault(); fn(); }
  });

  stage.addEventListener("wheel", (e) => {
    if (!e.ctrlKey) return;
    e.preventDefault();
    zoomBy(e.deltaY < 0 ? 1.15 : 1 / 1.15);
  }, { passive: false });

  stage.addEventListener("dblclick", () => { zoom === 1 ? (zoom = 2.5, apply()) : fit(); });

  let drag = null;
  stage.addEventListener("pointerdown", (e) => {
    if (zoom <= 1) return;
    drag = { x: e.clientX - pan.x, y: e.clientY - pan.y };
    stage.classList.add("dragging");
    stage.setPointerCapture(e.pointerId);
  });
  stage.addEventListener("pointermove", (e) => {
    if (!drag) return;
    pan = { x: e.clientX - drag.x, y: e.clientY - drag.y };
    apply();
  });
  ["pointerup", "pointercancel"].forEach((ev) => stage.addEventListener(ev, () => {
    drag = null;
    stage.classList.remove("dragging");
  }));

  document.addEventListener("visibilitychange", () => {
    if (!playing) return;
    if (document.hidden) clearInterval(timer);
    else resetTimer();
  });

  updateMeta();
  apply();
})();