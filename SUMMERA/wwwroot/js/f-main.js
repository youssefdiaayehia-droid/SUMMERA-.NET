/* ============================================
   CUSTOM CURSOR
   ============================================ */
const dot = document.getElementById("cursor-dot");
const ring = document.getElementById("cursor-ring");

if (dot && ring && window.matchMedia("(min-width: 861px)").matches) {
  let mouseX = 0, mouseY = 0, ringX = 0, ringY = 0;

  window.addEventListener("mousemove", (e) => {
    mouseX = e.clientX;
    mouseY = e.clientY;
    dot.style.left = mouseX + "px";
    dot.style.top = mouseY + "px";
  });

  function animateRing() {
    ringX += (mouseX - ringX) * 0.18;
    ringY += (mouseY - ringY) * 0.18;
    ring.style.left = ringX + "px";
    ring.style.top = ringY + "px";
    requestAnimationFrame(animateRing);
  }
  animateRing();

  document.querySelectorAll("a, button, .card").forEach((el) => {
    el.addEventListener("mouseenter", () => ring.classList.add("hover"));
    el.addEventListener("mouseleave", () => ring.classList.remove("hover"));
  });
}
// ============================================
// Navigation Sheet Logic
// ============================================
const menubtn = document.getElementById("menu-bar-but");
const closebtn = document.getElementById("more-close");
const overlay = document.getElementById("more-sheet-overlay");

function openMenu() {
    overlay.classList.add("open");
}

function closeMenu() {
    overlay.classList.remove("open");
}

menubtn.addEventListener("click", openMenu);
closebtn.addEventListener("click", closeMenu);
overlay.addEventListener("click", closeMenu);