document.addEventListener('DOMContentLoaded', () => {
    // 1. التحقق من تسجيل دخول الـ Admin
    const loggedInUser = JSON.parse(localStorage.getItem('loggedInUser'));
    const ADMIN_EMAIL = 'admin@summera.com';

    if (!loggedInUser || loggedInUser.email.toLowerCase() !== ADMIN_EMAIL) {
        alert('Access Denied! Admin rights required.');
        window.location.href = 'index.html';
        return;
    }


    const addItemForm = document.getElementById('addItemForm');
    const itemsListContainer = document.getElementById('admin-items-list');


    const defaultItems = [
        { id: 1, title: 'Steigenberger El Gouna', location: 'El Gouna, Red Sea', desc: 'Surrounded by turquoise lagoons and an 18-hole golf course.' },
        { id: 2, title: 'Four Seasons Sharm', location: 'Sharm El Sheikh, Sinai', desc: 'Oceanfront oasis framed by palms and the Red Sea reef.' },
        { id: 3, title: 'Old Cataract Palace', location: 'Aswan, Upper Egypt', desc: 'Historic 19th-century Victorian palace on the Nile.' }
    ];


    function getStoredItems() {
        const stored = localStorage.getItem('summeraDestinations');
        if (!stored) {
            localStorage.setItem('summeraDestinations', JSON.stringify(defaultItems));
            return defaultItems;
        }
        return JSON.parse(stored);
    }

    function renderAdminItems() {
        if (!itemsListContainer) return;
        const items = getStoredItems();

        if (items.length === 0) {
            itemsListContainer.innerHTML = '<p style="color: #ffffff;">No destinations added yet.</p>';
            return;
        }

        itemsListContainer.innerHTML = items.map((item, index) => `
            <div class="item-row">
                <div class="item-info">
                    <h4>${item.title}</h4>
                    <p>📍 ${item.location}</p>
                </div>
                <button class="delete-btn" onclick="deleteItem(${index})">Delete</button>
            </div>
        `).join('');
    }

    if (addItemForm) {
        addItemForm.addEventListener('submit', (e) => {
            e.preventDefault();

            const title = document.getElementById('item-title').value.trim();
            const location = document.getElementById('item-location').value.trim();
            const desc = document.getElementById('item-desc').value.trim();

            const items = getStoredItems();
            items.push({ id: Date.now(), title, location, desc });

            localStorage.setItem('summeraDestinations', JSON.stringify(items));
            addItemForm.reset();
            renderAdminItems();
            alert('Destination added successfully!');
        });
    }

    window.deleteItem = function(index) {
        const items = getStoredItems();
        items.splice(index, 1);
        localStorage.setItem('summeraDestinations', JSON.stringify(items));
        renderAdminItems();
    };

    renderAdminItems();
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

/* ============================================
   Navigation Sheet Logic
   ============================================ */
const menubtn = document.getElementById("menu-bar-but");
const closebtn = document.getElementById("more-close");
const overlay = document.getElementById("more-sheet-overlay");

if (menubtn && closebtn && overlay) {
    function openMenu() {
        overlay.classList.add("open");
    }

    function closeMenu() {
        overlay.classList.remove("open");
    }

    menubtn.addEventListener("click", openMenu);
    closebtn.addEventListener("click", closeMenu);
    overlay.addEventListener("click", closeMenu);
}