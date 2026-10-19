// ============================================
// Wait for DOM to be fully loaded
// ============================================
document.addEventListener("DOMContentLoaded", function () {

    // ============================================
    // 1. CUSTOM CURSOR
    // ============================================
    const dot = document.getElementById("cursor-dot");
    const ring = document.getElementById("cursor-ring");

    if (dot && ring && window.matchMedia("(min-width: 861px)").matches) {
        let mouseX = window.innerWidth / 2;
        let mouseY = window.innerHeight / 2;
        let ringX = mouseX;
        let ringY = mouseY;

        // Initialize position
        dot.style.left = mouseX + "px";
        dot.style.top = mouseY + "px";
        ring.style.left = ringX + "px";
        ring.style.top = ringY + "px";

        document.addEventListener("mousemove", (e) => {
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

        // Hover effect on interactive elements
        const hoverTargets = document.querySelectorAll("a, button, .carousel-card, .cards-container, .hero-btn, .overlay-link");
        hoverTargets.forEach((el) => {
            el.addEventListener("mouseenter", () => ring.classList.add("hover"));
            el.addEventListener("mouseleave", () => ring.classList.remove("hover"));
        });
    }

    // ============================================
    // 2. NAVIGATION MENU
    // ============================================
    const menubtn = document.getElementById("menu-bar-but");
    const closebtn = document.getElementById("more-close");
    const overlay = document.getElementById("more-sheet-overlay");

    if (menubtn && overlay) {
        menubtn.addEventListener("click", (e) => {
            e.stopPropagation();
            overlay.classList.add("open");
        });
    }

    if (closebtn && overlay) {
        closebtn.addEventListener("click", () => {
            overlay.classList.remove("open");
        });
    }

    if (overlay) {
        overlay.addEventListener("click", (e) => {
            if (e.target === overlay) {
                overlay.classList.remove("open");
            }
        });
    }

    // ============================================
    // 3. DYNAMIC HOTEL CARD MODAL
    // ============================================
    const hotelModal = document.getElementById("hotel-modal");
    const hotelModalText = document.getElementById("hotel-modal-text");
    const hotelModalClose = document.getElementById("hotel-modal-close");
    const allHotelCards = document.querySelectorAll(".carousel-card");

    if (hotelModal && hotelModalText && allHotelCards.length > 0) {
        allHotelCards.forEach((card) => {
            card.addEventListener("click", (e) => {
                e.stopPropagation();

                // Remove active from all
                allHotelCards.forEach(c => c.classList.remove("active"));

                // Add active to clicked
                card.classList.add("active");

                // Set description text
                hotelModalText.textContent = card.dataset.description || "";

                // Show modal
                hotelModal.classList.add("open");
            });
        });

        // Close modal function
        function closeHotelModal() {
            allHotelCards.forEach(c => c.classList.remove("active"));
            hotelModal.classList.remove("open");
        }

        if (hotelModalClose) {
            hotelModalClose.addEventListener("click", closeHotelModal);
        }

        hotelModal.addEventListener("click", (e) => {
            if (e.target === hotelModal) {
                closeHotelModal();
            }
        });
    }

});