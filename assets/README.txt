PicPic icon pack

picpic-icon.svg          Master app icon (rounded, dark)
picpic-icon-light.svg    Light background version
picpic-icon-square.svg   Square, no rounding - give this to app stores (iOS/Android round it themselves)
picpic-mark.svg          Mark only, transparent background
picpic-mono-black/white  One-colour versions
picpic-favicon.svg       Simplified art for tiny sizes
favicon.ico              16/32/48 favicon
png/                     Ready PNGs (apple-touch-icon 180, 192/512 for web manifest, 1024 store)

Website <head> (site/index.html; the Pages workflow copies these files to the site root):
<link rel="icon" href="favicon.ico" sizes="any">
<link rel="icon" href="picpic-favicon.svg" type="image/svg+xml">
<link rel="apple-touch-icon" href="apple-touch-icon.png">

Colours: Navy #12122A  Pink #FF3D6E  Cyan #22D3FF  Yellow #FFE14D
