document.addEventListener('DOMContentLoaded', () => {
    const loginForm = document.getElementById('loginForm') || document.querySelector('.login-form');

    if (loginForm) {
        loginForm.addEventListener('submit', (e) => {
            e.preventDefault();

            const emailInput = document.getElementById('email') || document.getElementById('login-email');
            const passwordInput = document.getElementById('password') || document.getElementById('login-password');

            if (!emailInput || !passwordInput) {
                alert('Email or Password input missing!');
                return;
            }

            const email = emailInput.value.trim().toLowerCase();
            const password = passwordInput.value;

            const registeredUsers = JSON.parse(localStorage.getItem('registeredUsers')) || [];

            if (registeredUsers.length === 0) {
                alert('No registered accounts found! Please register first.');
                window.location.href = 'register.html';
                return;
            }

            const matchedUser = registeredUsers.find(
                user => user.email.toLowerCase() === email && user.password === password
            );

            if (matchedUser) {
                localStorage.setItem('loggedInUser', JSON.stringify({
                    name: matchedUser.name,
                    email: matchedUser.email
                }));
                
                // التوجه لصفحة الأدمن إذا كان الإيميل هو إيميل الأدمن
                if (matchedUser.email.toLowerCase() === 'admin@summera.com') {
                    alert('Welcome Admin!');
                    window.location.href = 'admin.html';
                } else {
                    alert('Welcome back, ' + matchedUser.name + '!');
                    window.location.href = 'profile.html';
                }
            } else {
                alert('Invalid Email or Password!');
            }
        });
    }
});
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
