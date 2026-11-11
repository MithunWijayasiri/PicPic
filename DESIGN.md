---
name: PicPic Site
description: "A slate-desktop landing page where a wallpaper image is the ground, glass chrome floats above it, and one amber action carries the download."
colors:
  slate: "#101820"
  slate-2: "#16212b"
  slate-3: "#1c2939"
  footer: "#0c1219"
  bone: "#f2f0eb"
  bone-dim: "#b9c2cc"
  line: "rgba(242, 240, 235, 0.16)"
  line-strong: "rgba(242, 240, 235, 0.28)"
  amber: "#ffe14d"
  amber-ink: "#101820"
  amber-on-light: "#7a6200"
  paper: "#e6e2d4"
  ink-dim: "#47525f"
  scrollbar-thumb: "#3a4a5a"
typography:
  display:
    fontFamily: "Archivo, system-ui, -apple-system, Segoe UI, sans-serif"
    fontSize: "clamp(46px, 8vw, 92px)"
    fontWeight: 800
    lineHeight: 0.98
    letterSpacing: "-0.035em"
  headline:
    fontFamily: "Archivo, system-ui, -apple-system, Segoe UI, sans-serif"
    fontSize: "clamp(30px, 4.6vw, 46px)"
    fontWeight: 800
    lineHeight: 1.05
    letterSpacing: "-0.025em"
  title:
    fontFamily: "Archivo, system-ui, -apple-system, Segoe UI, sans-serif"
    fontSize: "19px"
    fontWeight: 700
    lineHeight: 1.3
    letterSpacing: "-0.01em"
  body:
    fontFamily: "Archivo, system-ui, -apple-system, Segoe UI, sans-serif"
    fontSize: "16px"
    fontWeight: 400
    lineHeight: 1.65
  label:
    fontFamily: "JetBrains Mono, ui-monospace, Cascadia Code, Consolas, monospace"
    fontSize: "12px"
    fontWeight: 500
    lineHeight: 1.4
    letterSpacing: "0.12em"
rounded:
  sm: "10px"
  md: "12px"
  lg: "14px"
  xl: "18px"
  pill: "999px"
spacing:
  xs: "10px"
  sm: "14px"
  md: "24px"
  lg: "48px"
  section: "96px"
components:
  button-primary:
    backgroundColor: "{colors.amber}"
    textColor: "{colors.amber-ink}"
    rounded: "{rounded.lg}"
    padding: "18px 30px"
    height: "58px"
  button-primary-hover:
    backgroundColor: "{colors.amber}"
    textColor: "{colors.amber-ink}"
    rounded: "{rounded.lg}"
    padding: "18px 30px"
    height: "58px"
  button-ghost:
    backgroundColor: "rgba(16, 24, 32, 0.35)"
    textColor: "{colors.bone}"
    rounded: "{rounded.lg}"
    padding: "14px 22px"
    height: "48px"
  button-ghost-hover:
    backgroundColor: "rgba(16, 24, 32, 0.35)"
    textColor: "{colors.amber}"
    rounded: "{rounded.lg}"
    padding: "14px 22px"
    height: "48px"
  button-small:
    backgroundColor: "rgba(16, 24, 32, 0.35)"
    textColor: "{colors.bone}"
    rounded: "{rounded.sm}"
    padding: "10px 16px"
    height: "38px"
  chip-format:
    backgroundColor: "rgba(16, 24, 32, 0.35)"
    textColor: "{colors.bone}"
    rounded: "{rounded.sm}"
    padding: "14px 10px"
---

# Design System: PicPic Site

## Overview

**Creative North Star: "Desktop Wallpaper Ritual"**

The image is the desktop. The page refuses the SaaS hero-plus-card template and instead puts a wallpaper photograph as the ground, with chrome floating above it. The first viewport is full-bleed image with object-fit cover, bottom-anchored copy left-aligned, and a floating taskbar nav on top; deep slate is only where image is not.

A visitor who opens images daily recognizes the desktop instantly, reads the offer in one line, and can download without hunting past the four feature boxes. Translucent glass chrome, bone type, and one amber action keep the interface achromatic; the image carries most color. Below the hero every section sits on the same bone ground with slate ink; the dark blocks are the feature boxes, format chips, plugin panel, checklist items, ghost buttons, link-row icons, nav, footer, and the hero photo itself. Formats and plugins read as named rows, not tiles. Below the fold the structure stays static — four feature boxes, format groups, plugin links, and a release block — with no live viewer and no drag-drop. Motion is a single authored moment: damped hero parallax, and nothing else staggers in.

**Key Characteristics:**
- Wallpaper photograph as the ground; slate only where image is not
- Floating translucent taskbar chrome over a bottom-anchored hero
- Bone type on deep slate and slate ink on bone light bands; one amber action
- Image carries color; interface stays achromatic except Download
- Four compact feature boxes; formats and plugins as named rows
- One damped parallax moment; no entrance staggers, no glow

## Colors

A near-monochrome slate-and-bone system with a single amber action, letting photography supply all other color.

### Primary
- **Amber** (#ffe14d): The only chromatic interface color. Primary action fill, hover and focus accents, status dots, icon strokes, and active states. Its rarity is the point.

### Neutral
- **Deep Slate** (#101820): Page ground, amber-ink text on amber fills, scrollbar track.
- **Raised Slate** (#16212b): Inset panel and card fill on Bone Ground (feature boxes, plugin panel, checklist items).
- **Slate Lift** (#1c2939): Highest slate step in the scale.
- **Footer Slate** (#0c1219): Footer ground, one step darker than the page.
- **Bone** (#f2f0eb): Primary text and headings.
- **Dim Bone** (#b9c2cc): Secondary text, descriptions, inactive nav.
- **Hairline** (rgba(242, 240, 235, 0.16)): Default borders and dividers.
- **Hairline Strong** (rgba(242, 240, 235, 0.28)): Emphasized borders on ghost controls and tags.
- **Scrollbar Slate** (#3a4a5a): Scrollbar thumb on Deep Slate.
- **Amber Ink** (#101820): Text on amber fills; intentionally identical to Deep Slate.
- **Bone Ground** (#e6e2d4): Uniform ground for all below-hero sections; headings and prose on it use Deep Slate (#101820) ink.
- **Ink Dim** (#47525f): Secondary text on Bone Ground.
- **Amber on Light** (#7a6200): The release-heading accent word, hover arrow, release-note link, and focus outlines on Bone Ground, darkened from Amber (#ffe14d) for legibility. All other amber accents sit on dark fills and stay Amber (#ffe14d).

Bone and Dim Bone on Deep Slate meet at least 4.5:1 for body text and 3:1 for large text. On Bone Ground, Deep Slate ink and Ink Dim meet 4.5:1, and Amber on Light meets 3:1 for icon strokes, focus outlines, and hover accents.

### Named Rules
**The One Amber Rule.** Amber appears only on the primary action and its supporting accents (hover, focus, active, status, icon stroke). Everything else is slate, bone, or hairline.
**The Image Carries Color Rule.** The interface adds no color of its own beyond amber; photography is the color source. If a surface is not image, it is achromatic.

## Typography

**Display Font:** Archivo (with system-ui, -apple-system, Segoe UI, sans-serif fallback)
**Body Font:** Archivo (with system-ui, -apple-system, Segoe UI, sans-serif fallback)
**Label/Mono Font:** JetBrains Mono (with ui-monospace, Cascadia Code, Consolas, monospace fallback)

**Character:** One grotesque carries both display and body at heavy-to-regular weights with tight display spacing; JetBrains Mono is reserved strictly for data and status labels, set uppercase with wide tracking and tabular figures.

### Hierarchy
- **Display** (800, clamp(46px, 8vw, 92px), 0.98, -0.035em): Hero headline only, capped at a 14ch measure, balanced wrapping.
- **Headline** (800, clamp(30px, 4.6vw, 46px), 1.05, -0.025em): Section headlines.
- **Title** (700, 19px, 1.3, -0.01em): Row and panel headings, including format-group titles at 18px; feature box titles run at 17px.
- **Body** (400, 16px, 1.65): Base reading text; descriptions cap at about 62ch. The hero sub runs clamp(16px, 1.8vw, 19px) at 1.55 over at most 60ch.
- **Label** (500, 12px, 1.4, 0.12em uppercase): Mono status and tag text with tabular-nums. Format chips run 500 14px in the same mono face.

### Named Rules
**The Mono-For-Data-Only Rule.** JetBrains Mono is used only for data, status, tags, and format names — never for prose, headings, or buttons.

## Layout

A single 1120px wrap with 24px inline padding, sections inset 24px inline with the wrap padding zeroed inside them (96px vertical rhythm, 72px at 640px and below), and hairline top borders between sections. The hero is 100svh, full-bleed, flex-end anchored with 64px bottom padding, copy capped by the headline and 60ch sub measures. Every below-hero section shares the same Bone Ground; grouping comes from the dark Raised Slate blocks (feature boxes, plugin panel, checklist items) and Deep Slate chips and buttons inset into it, with the footer in Footer Slate closing the page. Content density is airy: 48px section-head blocks, 40px gaps between format groups, 56px two-column gaps for plugins and release that collapse to one column at 1020px. Nav links hide and a menu button appears at 860px; hero buttons go full-width at 640px, and feature boxes step four-across, two at 1020px, and one at 640px; the format grid steps down at 640px and 460px. Breakpoints are 1020px, 860px, 640px, and 460px. Motion lives here as spatial behavior: the hero image drifts with damped parallax on scroll (0.12 lerp follow, exponential ease-out settle, -0.08 translateY factor, killed under reduced motion), and hover lifts are limited to translateY(-2px). No section entrance staggers exist.

## Elevation & Depth

This system has no glow, no halos, and no ambient shadow vocabulary. Depth is conveyed by translucent backdrop blur on the nav, a bottom-anchored scrim gradient over the hero image for legibility, dark Raised Slate panels inset into the bone ground, and hairline borders that carry grouping. The only shadows in the system are offset-plus-blur drops under the primary action and its hover state.

### Shadow Vocabulary
- **Action Rest** (`box-shadow: 0 16px 36px -14px rgba(0, 0, 0, 0.65)`): Primary button at rest.
- **Action Hover** (`box-shadow: 0 20px 44px -14px rgba(0, 0, 0, 0.65)`): Primary button on hover; the ghost button gains no shadow, only an amber border and text shift.

### Named Rules
**The Action-Only Shadow Rule.** Shadows appear only on the primary action and never as ambient glow. Everything else stays flat and is grouped by hairlines and tone.

## Shapes

Soft, utilitarian corners with no hard edge anywhere: 14px on buttons, feature icons, and checklist rows; 12px on list rows and link-row icons; 10px on chips and small buttons; 18px on the plugin panel and feature boxes; pill (999px) on tags and counts. Dots are circles (50%) at 7px for status and 3px radius at 9px for plugin marks. Icons sit 44px–48px in bordered squares with 1.7px stroke. Borders are 1px hairlines throughout.

## Components

### Buttons
- **Shape:** Generously rounded rectangles (14px radius, 10px at small size)
- **Primary:** Amber fill (#ffe14d) with slate ink (#101820), 58px minimum height, 18px 30px padding, 700 weight at 17px, plus the Action Rest shadow.
- **Hover / Focus:** Lifts translateY(-2px) with the Action Hover shadow; active presses to translateY(0) scale(0.98); focus-visible is a 2px amber outline offset 3px.
- **Secondary / Ghost / Tertiary:** Ghost is translucent slate (rgba(16, 24, 32, 0.35)) with a Hairline Strong border, 48px minimum height, 14px 22px padding, bone text; hover shifts border and text to amber. The small variant is 38px minimum height, 10px 16px padding, 13.5px text, 10px radius.

### Chips
- **Style:** Translucent slate fill (rgba(16, 24, 32, 0.35)), bone mono text (500 14px, tabular-nums), 1px hairline border, 10px radius, 14px 10px padding, centered.
- **State:** Hover shifts border and text to amber and lifts translateY(-2px). Format chips are static labels in a strict auto-fill grid (minimum 112px columns, 10px gap).

### Cards / Containers
- **Corner Style:** 18px on the plugin panel and feature boxes; 14px on checklist rows; 12px on plugin list rows.
- **Background:** Raised Slate (#16212b) on the plugin panel, feature boxes, and checklist items (dark blocks on the bone ground); rgba(242, 240, 235, 0.03) on plugin rows inside the dark panel.
- **Shadow Strategy:** None. Grouping is carried by 1px hairline borders, not elevation (see Elevation & Depth).
- **Border:** 1px hairline on every container.
- **Internal Padding:** 26px on the plugin panel (20px at 460px and below); 24px on feature boxes; 16px on checklist rows; 13px 14px on plugin list rows.

### Navigation
Fixed glass taskbar, 64px tall, translucent slate (rgba(16, 24, 32, 0.55)) with 14px backdrop blur, deepening to rgba(16, 24, 32, 0.88) with a hairline bottom border once scrolled. Brand mark with 800 20px name; bone-dim 14px 600 links with 9px radius, bone text with a faint bone wash on hover, amber when active. The mobile menu drops below the bar as a translucent column with hairline-separated links and an amber final link.

### Feature Boxes
Compact dark panels (Raised Slate, 24px padding, 18px radius, 1px hairline) inset into the bone ground in a four-across grid (two at 1020px, one at 640px): a 48px bordered icon square with amber stroke and faint amber wash on top, a 700 17px title over a 14px dim description. Hover lifts translateY(-3px) with an amber-tinted border. The four owner-directed boxes are Basic editing, Slideshow, Print &amp; PDF, and Image details; no thumbnails.

### Plugin Rows
Static list items (13px 14px padding, 12px radius, 1px hairline) with a 9px amber square mark at 3px radius, a 600 13px mono filename, and a 12.5px dim description. Link rows above them pair a 44px bordered icon square with bold 15.5px text, 13.5px dim support text, and a dim arrow that turns amber on hover while the row shifts translateX(4px).

### Checklist Items
Static rows (16px padding, 14px radius, 1px hairline) with a 26px amber circle check in slate ink, 700 15px bold text, and 13px dim support text. The current-step variant swaps the filled circle for an amber-outlined circle holding a pulse dot, with an amber-tinted border and amber bold text.

### Status Strip
Uppercase mono label (500 12px, 0.12em tracking, tabular-nums) in dim bone with bone bold values separated by dimmed slashes, a 7px amber status dot, and hairline spacing. The same mono treatment caps the footer with a hairline top border and centered caption, and appears in panel heads and counts.

### Browser Surfaces
Selection is amber fill with slate ink. The scrollbar is 10px wide with a #3a4a5a thumb at 8px radius on Deep Slate. Focus-visible is a 2px amber outline offset 3px at 4px radius. Links carry 1px underline thickness at 3px offset.

### Named Rules
**The One Largest Action Rule.** Download is the largest control on the page — 58px primary against 48px ghost and 38px small — and nothing else competes with it in size or color.

## Do's and Don'ts

### Do:
- **Do** use Download as the largest control (58px primary, amber fill) against smaller ghost (48px) and small (38px) controls.
- **Do** keep the image as the ground (object-fit cover, 100svh hero) with the scrim gradient carrying legibility over photography.
- **Do** keep mono for data only — JetBrains Mono for status, tags, counts, and format names, never for prose or headings.
- **Do** keep the interface achromatic except Download: slate, bone, and hairlines everywhere, amber only on the action and its accents.

### Don't:
- **Don't** use eyebrows or kickers above headlines; headlines stand on their own at 800 weight.
- **Don't** use gradient text; the single amber accent word and the scrim gradient are the only gradients in the system.
- **Don't** add more than the four owner-directed feature boxes; they stay compact slate panels with hairline borders, not a card wall.
- **Don't** use section numbers; numbered discipline belongs to format groups and data, not to headlines.
- **Don't** use paper, glow, or neon; no halos, no ambient shadow, no chromatic chrome — image carries all other color.
- **Don't** add new commercial claims to the visual system; the world describes presentation only, with no benchmarks, testimonials, or counts.
