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




/* ============================================
   PROFILE PAGE LOGIC
   ============================================ */
document.addEventListener('DOMContentLoaded', () => {
    // 1. التحقق من وجود جلسة تسجيل دخول نشطة
    const loggedInUser = JSON.parse(localStorage.getItem('loggedInUser'));

    if (!loggedInUser) {
        // تحويل المستخدم لصفحة تسجيل الدخول إذا لم يكن مسجلاً
        window.location.href = 'login.html';
        return;
    }

    // 2. عرض البيانات الشخصية للمستخدم
    const userName = loggedInUser.name || 'User';
    const userEmail = loggedInUser.email || '';
    
    const nameElem = document.getElementById('user-name');
    const emailElem = document.getElementById('user-email');
    const avatarElem = document.getElementById('user-avatar');

    if (nameElem) nameElem.textContent = userName;
    if (emailElem) emailElem.textContent = userEmail;
    if (avatarElem) avatarElem.textContent = userName.charAt(0).toUpperCase();

    // 3. التبديل بين التبويبات (Tabs)
    const tabs = document.querySelectorAll('.tab-btn');
    const contents = document.querySelectorAll('.tab-content');

    tabs.forEach(tab => {
        tab.addEventListener('click', () => {
            tabs.forEach(t => t.classList.remove('active'));
            contents.forEach(c => c.classList.remove('active'));

            tab.classList.add('active');
            const targetContent = document.getElementById(tab.dataset.tab);
            if (targetContent) {
                targetContent.classList.add('active');
            }
        });
    });

    // 4. عرض القائمة المحفوظة (Saved Items)
    function renderSavedItems() {
        const savedContainer = document.getElementById('saved-items-list');
        if (!savedContainer) return;

        const savedItems = JSON.parse(localStorage.getItem('savedItems')) || [];

        if (savedItems.length === 0) {
            savedContainer.innerHTML = '<p class="empty-msg">No saved destinations or hotels yet.</p>';
            return;
        }

        savedContainer.innerHTML = savedItems.map((item, index) => `
            <div class="saved-card" style="background: rgba(255,255,255,0.1); padding: 15px; border-radius: 10px; margin-bottom: 10px; display: flex; justify-content: space-between; align-items: center; color: #fff;">
                <div class="saved-info">
                    <h4 style="margin-bottom: 5px; color: #fff;">${item.title}</h4>
                    <p style="font-size: 0.85rem; color: #d9e4ec;">📍 ${item.location || 'Egypt'}</p>
                </div>
                <button class="remove-btn" onclick="removeSavedItem(${index})" style="background: #ef4444; color: white; border: none; padding: 6px 12px; border-radius: 6px; cursor: pointer;">Remove</button>
            </div>
        `).join('');
    }

    // 5. دالة حذف عنصر من المحفوظات
    window.removeSavedItem = function(index) {
        let savedItems = JSON.parse(localStorage.getItem('savedItems')) || [];
        savedItems.splice(index, 1);
        localStorage.setItem('savedItems', JSON.stringify(savedItems));
        renderSavedItems();
    };

    // 6. تسجيل الخروج (Logout)
    const logoutBtn = document.getElementById('logoutBtn');
    if (logoutBtn) {
        logoutBtn.addEventListener('click', () => {
            localStorage.removeItem('loggedInUser');
            window.location.href = 'login.html';
        });
    }

    renderSavedItems();
});