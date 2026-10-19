document.addEventListener('DOMContentLoaded', () => {

    const menuBtn = document.querySelector('.menu-btn');
    const moreSheetOverlay = document.getElementById('more-sheet-overlay');
    const moreCloseBtn = document.getElementById('more-close');

    if (menuBtn && moreSheetOverlay) {
        menuBtn.addEventListener('click', () => {
            moreSheetOverlay.classList.add('open');
        });
    }

    if (moreCloseBtn && moreSheetOverlay) {
        moreCloseBtn.addEventListener('click', () => {
            moreSheetOverlay.classList.remove('open');
        });
    }

    if (moreSheetOverlay) {
        moreSheetOverlay.addEventListener('click', (e) => {
            if (e.target === moreSheetOverlay) {
                moreSheetOverlay.classList.remove('open');
            }
        });
    }

// 2. فلترة الفنادق حسب المدينة وتاريخ الحجز (Hotels Search Filter)
const searchForm = document.querySelector('.search-bar');
const citySelect = document.getElementById('city');
const checkinInput = document.getElementById('checkin');
const hotelCards = document.querySelectorAll('.hotel-card');

if (searchForm) {
    searchForm.addEventListener('submit', (e) => {
        e.preventDefault();
        
        const selectedCity = citySelect ? citySelect.value.toLowerCase() : '';
        const checkinDate = checkinInput ? checkinInput.value : '';

        // حفظ تاريخ الحجز في ذاكرة المتصفح لاستخدامه لاحقاً في صفحة الحجز والتفاصيل
        if (checkinDate) {
            localStorage.setItem('selectedCheckinDate', checkinDate);
        }

        hotelCards.forEach(card => {
            const locationText = card.querySelector('.location') ? card.querySelector('.location').textContent.toLowerCase() : '';

            // الفلترة بناءً على المدينة المختارة
            if (selectedCity === '' || locationText.includes(selectedCity)) {
                card.style.display = 'flex';
            } else {
                card.style.display = 'none';
            }
        });
    });
}

    // 3. التوجيه لصفحة التاصيل عند الضغط على أزرار الحجز
    const bookButtons = document.querySelectorAll('.hotel-card .book-btn');
    bookButtons.forEach((button, index) => {
        button.addEventListener('click', () => {
            // الانتقال لصفحة التفاصيل وتمرير معرف الفندق (1, 2, 3...)
            window.location.href = `hotels-details.html?id=${index + 1}`;
        });
    });

    // 4. قاعدة بيانات الفنادق وعرض البيانات ديناميكياً في صفحة التفاصيل (details.html)
    const hotelsData = {
        "1": {
            name: "Cairo Grand Hotel",
            location: "17 26 July Street, Al Azbakeya, Cairo",
            stars: "⭐⭐⭐ 3-Star",
            price: "$39 / night",
            image: "https://images.unsplash.com/photo-1566073771259-6a8506099945?auto=format&fit=crop&w=1200&q=80",
            description: "Overlooking a bustling intersection in Downtown Cairo, this relaxed hotel is 20 km from Cairo International Airport and a kilometer from the iconic Tahrir Square.",
            mapUrl: "https://www.google.com/maps/place/Grand+Hotel+Cairo/@30.0527581,31.2396536,17z"
        },
        "2": {
            name: "Alexandria Palace",
            location: "El-Gaish Road, El-Shohada, Al Attarin, Alexandria",
            stars: "⭐⭐⭐⭐ 4-Star",
            price: "$91 / night",
            image: "https://cf.bstatic.com/xdata/images/hotel/max1024x768/346342256.jpg?k=2445da681ac5c5d211e073b1cf042b947c8270f564c1948429dc9d5c3555e831&o=",
            description: "Helnan Palace Hotel - Montaza Royal Gardens Alexandria offers a 5-star experience in Alexandria with sea views and luxury services.",
            mapUrl: "https://www.google.com/maps"
        },
        "3": {
            name: "Luxor Nile Hotel",
            location: "Khaled Ibn El Walid Street, Luxor",
            stars: "⭐⭐⭐⭐⭐ 5-Star",
            price: "$161 / night",
            image: "https://cf.bstatic.com/xdata/images/hotel/max1024x768/520473480.jpg?k=9f79407bd7cdec0e8ef40f6157f597ebb293855809209278c4253ec23ad150db&o=",
            description: "Nile Palace Luxor Hotel is conveniently located within a short walk of Luxor's large shopping district.",
            mapUrl: "https://www.google.com/maps"
        },
        "4": {
            name: "Aswan Nile Hotel",
            location: "El-Zawya El-Hamraa El-Balad, Aswan",
            stars: "⭐⭐⭐⭐ 4-Star",
            price: "$54 / night",
            image: "https://cf.bstatic.com/xdata/images/hotel/max1024x768/874071701.jpg?k=1403c624487f04bb14ebba77a1c6298d3a5a01cc212120fa79b379a0ac8825e7&o=",
            description: "Guests can enjoy a terrace and restaurant, complemented by free WiFi and a beautiful view of the Nile.",
            mapUrl: "https://www.google.com/maps"
        },
        "5": {
            name: "Siwa Oasis Lodge",
            location: "Siwa Oasis, Marsa Matrouh",
            stars: "⭐⭐⭐ 3-Star",
            price: "$49 / night",
            image: "https://q-xx.bstatic.com/xdata/images/xphoto/608x352/45450098.webp?k=99ff43e6b3071cd7f65649c782eea4f12b0505bcad1f03f3f46e67243be2dec6&o=",
            description: "Siwa Palace Lodge in Marsa Matrouh offers an authentic desert retreat experience with natural hot springs.",
            mapUrl: "https://www.google.com/maps"
        },
        "6": {
            name: "Fayoum Lake Hotel",
            location: "Lake Qarun Tourist Road, near Tunis Village, Fayoum",
            stars: "⭐⭐⭐⭐ 4-Star",
            price: "$70 / night",
            image: "https://cf.bstatic.com/xdata/images/hotel/max1024x768/565130895.jpg?k=8359ac9645758ba75b6fd4475bf3eb56cb93b92ebd76b14571fd67954d646b1d&o=",
            description: "A peaceful desert oasis hotel where all rooms face Lake Qarun.",
            mapUrl: "https://www.google.com/maps"
        }
    };

    // قراءة الـ id من رابط الصفحة وتعبئة البيانات
    const urlParams = new URLSearchParams(window.location.search);
    const hotelId = urlParams.get('id');

    if (hotelId && hotelsData[hotelId]) {
        const currentHotel = hotelsData[hotelId];

        const titleEl = document.querySelector('.hotel-title');
        const starsEl = document.querySelector('.stars');
        const locationTextEl = document.querySelector('.map-text p');
        const locationNameEl = document.querySelector('.map-text strong');
        const priceEl = document.querySelector('.price-info strong');
        const descEl = document.querySelector('.description-text');
        const heroImgEl = document.querySelector('.hero-img');
        const mapLinkEl = document.querySelector('.map-link');

        if (titleEl) titleEl.textContent = currentHotel.name;
        if (starsEl) starsEl.textContent = currentHotel.stars;
        if (locationNameEl) locationNameEl.textContent = currentHotel.name;
        if (locationTextEl) locationTextEl.textContent = currentHotel.location;
        if (priceEl) priceEl.textContent = currentHotel.price;
        if (descEl) descEl.textContent = currentHotel.description;
        if (heroImgEl) heroImgEl.src = currentHotel.image;
        if (mapLinkEl) mapLinkEl.href = currentHotel.mapUrl;

        document.title = `${currentHotel.name} - SUMMERA`;
    }

    // 5. زر تأكيد الحجز داخل صفحة التفاصيل
    const reserveBtn = document.querySelector('.reserve-btn');
    if (reserveBtn) {
        reserveBtn.addEventListener('click', () => {
            window.location.href = 'booking.html'; // قم بتغييرها لاسم صفحة الحجز لديك
        });
    }
});