---
name: PicPic Site
description: "A Windows 11 Fluent Glass landing page where dark Mica and Acrylic chrome frames the picture viewer, crisp light ground surfaces data, and vivid Amber directs action."
colors:
  hero-ground: "#0b1118"
  card-dark: "#121b26"
  card-dark-hover: "rgba(245, 158, 11, 0.3)"
  light-ground: "#f8fafc"
  card-light: "#ffffff"
  slate-ink: "#0f172a"
  ink-muted: "#64748b"
  hairline-dark: "rgba(255, 255, 255, 0.08)"
  hairline-light: "#e2e8f0"
  amber: "#f59e0b"
  amber-hover: "#fbbf24"
  amber-ink: "#0f172a"
  amber-on-light: "#7a6200"
typography:
  display:
    fontFamily: "Plus Jakarta Sans, system-ui, -apple-system, Segoe UI, sans-serif"
    fontSize: "clamp(46px, 8vw, 84px)"
    fontWeight: 800
    lineHeight: 1.02
    letterSpacing: "-0.035em"
  headline:
    fontFamily: "Plus Jakarta Sans, system-ui, -apple-system, Segoe UI, sans-serif"
    fontSize: "clamp(30px, 4.6vw, 44px)"
    fontWeight: 800
    lineHeight: 1.1
    letterSpacing: "-0.025em"
  title:
    fontFamily: "Plus Jakarta Sans, system-ui, -apple-system, Segoe UI, sans-serif"
    fontSize: "20px"
    fontWeight: 700
    lineHeight: 1.3
    letterSpacing: "-0.015em"
  body:
    fontFamily: "Plus Jakarta Sans, system-ui, -apple-system, Segoe UI, sans-serif"
    fontSize: "16px"
    fontWeight: 400
    lineHeight: 1.65
  label:
    fontFamily: "JetBrains Mono, ui-monospace, Cascadia Code, Consolas, monospace"
    fontSize: "12px"
    fontWeight: 500
    lineHeight: 1.4
    letterSpacing: "0.1em"
rounded:
  sm: "6px"
  md: "10px"
  lg: "14px"
  xl: "20px"
  pill: "999px"
spacing:
  xs: "8px"
  sm: "12px"
  md: "24px"
  lg: "48px"
  section: "96px"
components:
  button-primary:
    backgroundColor: "{colors.amber}"
    textColor: "{colors.amber-ink}"
    rounded: "{rounded.lg}"
    padding: "16px 28px"
    height: "56px"
  button-ghost:
    backgroundColor: "rgba(255, 255, 255, 0.06)"
    textColor: "#ffffff"
    rounded: "{rounded.lg}"
    padding: "14px 22px"
    height: "48px"
  chip-format:
    backgroundColor: "{colors.card-light}"
    textColor: "{colors.slate-ink}"
    rounded: "{rounded.sm}"
    padding: "10px 14px"
---

# Design System: PicPic Site

## Overview

**Creative North Star: "Windows 11 Fluent Glass"**

PicPic is an agile, native Windows picture viewer built for fluid browsing and editing. The design system translates the Windows 11 Fluent Glass aesthetic to the web: dark Fluent Mica and frosted Acrylic chrome anchor the hero and app preview, while below-the-fold sections transition to a crisp cool slate light ground for effortless reading. Single-action vivid Amber provides focused contrast.

The hero presents an interactive-fidelity Windows 11 App Mockup with a native titlebar, window controls, canvas viewport, and a floating Acrylic HUD toolbar. Below the hero, crisp light surfaces, Bento feature cards, and structured format pills present product capabilities cleanly and transparently.

**Key Characteristics:**
- Dark Fluent Mica / Acrylic hero (`#0b1118`) with ambient radial glow.
- Interactive-fidelity Windows 11 window mockup with glass chrome and floating HUD.
- Crisp cool slate light ground (`#f8fafc`) below fold with high-contrast slate ink (`#0f172a`).
- Bento card grid architecture with subtle hover borders and elevation.
- Monospace format pills (`JetBrains Mono`) organized by category chips.
- Vibrant Amber (`#f59e0b`) primary accents; darkened Amber (`#7a6200`) for WCAG AAA accessibility on light ground.

---

## Colors

A balanced dual-ground palette pairing deep Windows 11 dark acrylic with crisp, modern light surfaces.

### Primary Accents
- **Amber Primary** (`#f59e0b`): The core interactive action color. Primary download CTA, active state indicator, focus rings on dark surfaces.
- **Amber Hover** (`#fbbf24`): Elevated hover state for dark-surface primary actions.
- **Amber Ink** (`#0f172a`): High-contrast dark ink on amber fills.
- **Amber on Light** (`#7a6200`): Darkened amber formulation for text accents, borders, tags, and focus rings on light backgrounds (ensures 4.5:1+ contrast).

### Dark Surfaces (Hero & Mockup Chrome)
- **Dark Hero Ground** (`#0b1118`): Deep Mica / Acrylic foundation ground for the hero and preview section.
- **Dark Card Surface** (`#121b26`): Surface fill for dark panels, floating bars, and mockup window frame.
- **Dark Hairline** (`rgba(255, 255, 255, 0.08)`): Crisp boundary divider and border on dark grounds.
- **Dark Hairline Hover** (`rgba(245, 158, 11, 0.3)`): Subtle amber border glow on interactive dark cards.

### Light Surfaces (Bento, Formats, Plugins, Release)
- **Crisp Light Ground** (`#f8fafc`): Cool slate-tinted ground for readable content bands.
- **Card Light Surface** (`#ffffff`): Pure white elevated card surfaces on light ground.
- **Light Hairline** (`#e2e8f0`): Refined card border on light grounds.
- **Slate Ink** (`#0f172a`): Primary reading ink and headings.
- **Ink Muted** (`#64748b`): Secondary descriptions, metadata, and helper text.

---

## Typography

**Display & Body Font:** Plus Jakarta Sans (with system-ui, -apple-system, Segoe UI, sans-serif fallback)
**Data & Code Font:** JetBrains Mono (with ui-monospace, Cascadia Code, Consolas, monospace fallback)

### Hierarchy
- **Display** (800, `clamp(46px, 8vw, 84px)`, line-height: 1.02, letter-spacing: -0.035em): Hero headline, punchy and commanding.
- **Headline** (800, `clamp(30px, 4.6vw, 44px)`, line-height: 1.1, letter-spacing: -0.025em): Section headings.
- **Title** (700, `20px`, line-height: 1.3, letter-spacing: -0.015em): Bento card titles, plugin group headings.
- **Body** (400, `16px`, line-height: 1.65): Standard reading prose and descriptions.
- **Label / Data** (500, `12px` or `13px`, line-height: 1.4, letter-spacing: 0.1em uppercase): JetBrains Mono tags, window metadata, release versions, and format chips.

---

## Windows 11 Fluent App Mockup Specifications

The hero mockup (`.mockup-wrap`, `.mockup-glow`, `.mockup-window`) models a modern Windows 11 desktop window:

1. **Titlebar (`.win-titlebar`):**
   - Height: 40px, frosted glass background with subtle bottom hairline border.
   - Title (`.win-title`): 12px Plus Jakarta Sans / JetBrains Mono file title with app identity icon.
   - Window Controls (`.win-controls`):
     - Min (`.win-min`), Max (`.win-max`), Close (`.win-close`).
     - Native Windows 11 vector glyphs with standard hover feedback (red fill for close).
2. **Canvas Viewport (`.win-canvas`):**
   - High-resolution sample imagery (`assets/demo-neon-dusk.jpg`) displaying rich color rendering.
   - Aspect ratio preserved with responsive containment and subtle inner vignette.
3. **Floating Acrylic HUD Toolbar (`.win-hud`):**
   - Centered floating pill toolbar anchored to the bottom of the canvas viewport.
   - Background: `rgba(18, 27, 38, 0.75)` with `backdrop-filter: blur(16px)` and 1px hairline border.
   - Control buttons (`.win-hud-btn`): Zoom in/out, rotate, 100% scale, fit, slideshow, and inspection info.
   - Status indicators (`.win-hud-info`): Image dimensions, color depth, and zoom percentage in JetBrains Mono.

---

## Bento Card & Format Pill Specifications

### Bento Grid (`.bento-grid`)
- Asymmetric responsive layout combining full-width, two-column (`.bento-col-6`), and single-column feature blocks.
- Surfaces use `.bento-card` with clean white fill (`#ffffff`) on `.section-light` or `#121b26` on dark sections.
- Rounded corners at `14px` (`border-radius: 14px`) with 1px border (`#e2e8f0` or `rgba(255, 255, 255, 0.08)`).
- Icon containers (`.bento-icon`): 48px rounded square with subtle amber stroke or tint.

### Format Chips & Pills (`.formats-box`, `.fmt-pill`)
- Categorized chip rows (`.chips-row`) grouping formats (e.g., Common, RAW, Web/Vector, Archive/Specialized).
- Format Pill (`.fmt-pill`):
  - Font: JetBrains Mono (500, 13px).
  - Background: Crisp light surface (`#ffffff`) or subtle glass on dark.
  - Border: 1px `#e2e8f0` with smooth transition on hover (`border-color: #f59e0b`, translateY(-1px)).

---

## Elevation & Depth

- **Frosted Acrylic / Glass Backdrop Blur:**
  - Standard glass chrome: `backdrop-filter: blur(16px) saturate(180%)`.
  - Floating HUD & taskbar nav: `backdrop-filter: blur(20px)`.
- **Ambient Radial Glows:**
  - Mockup backdrop glow (`.mockup-glow`): Subtle radial gradient (`radial-gradient(ellipse at 50% 50%, rgba(245, 158, 11, 0.12) 0%, transparent 70%)`) creating atmospheric depth without visual clutter.
- **Crisp Hairline Boundaries:**
  - Clean 1px solid boundaries define every elevated surface (`rgba(255, 255, 255, 0.08)` dark, `#e2e8f0` light).
  - Preserves precise Windows 11 Fluent geometric discipline.

---

## Accessibility & Focus System

- **Dark Surfaces:**
  - `:focus-visible` utilizes `outline: 2px solid #f59e0b; outline-offset: 3px; border-radius: 4px;`.
- **Light Surfaces (`.section-light`):**
  - `:focus-visible` utilizes `outline: 2px solid #7a6200; outline-offset: 3px; border-radius: 4px;`.
- **Contrast Ratios:**
  - All body text maintains >= 4.5:1 against respective ground surfaces.
  - Interactive targets and focus rings maintain >= 3:1 against background colors.
