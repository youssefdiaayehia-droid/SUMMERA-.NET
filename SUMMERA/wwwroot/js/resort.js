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
overlay.addEventListener("click", closeMenu)


// --- SEARCH BAR RESORTS ---
    const locationSelect = document.getElementById("location-select");
    const priceSelect = document.getElementById("price-select");
    const searchBtn = document.getElementById("search-button");
    const resortCards = document.querySelectorAll('.resorts-cards');

if (searchBtn && locationSelect && priceSelect) {
        searchBtn.addEventListener('click' , () =>{
        const selectedLocation = locationSelect.value.toLowerCase();
        const selectedPriceRange = priceSelect.value;

        resortCards.forEach(card =>{
        const cardLocation = (card.dataset.location || "").toLowerCase();
        const cardPrice = Number(card.dataset.price);


        const matchesLocation = (selectedLocation ==='all') || (cardLocation === selectedLocation);

    let matchesPrice = false;
    if (selectedPriceRange === 'all') {
        matchesPrice = true;
    }
    else if (selectedPriceRange === 'under-150' && cardPrice < 150) {
        matchesPrice = true;        
    }
    else if (selectedPriceRange === '150-250' && cardPrice >= 150 && cardPrice <= 250) {
        matchesPrice = true;        
    }
    else if (selectedPriceRange === '250-400' && cardPrice >= 250 && cardPrice <= 400) {
        matchesPrice = true;        
    }
    else if (selectedPriceRange === 'above-400' && cardPrice > 400) {
        matchesPrice = true;        
    }

if (matchesLocation && matchesPrice) {
                card.style.display = "flex";
            } else {
                card.style.display = "none";
            }
    });

    })
}

/*===============================
    RESORTS DETAILS 
================================*/

/* ============================================
   RESORTS DATABASE (ALL 6 LOCATIONS)
   ============================================ */
const resortsData = {
  "dahab": {
    name: "Dahab Lagoon Club & Resort",
    location: "📍 El Lisan, Dahab, South Sinai",
    rating: "⭐⭐⭐⭐ 4-Star Beachfront",
    price: "$120 / night",
    image: "images/Resorts/Dahab.png",
    overview: "Escape to a serene coastal sanctuary nestled along the golden shorelines of the Gulf of Aqaba. Dahab Lagoon Club & Resort features breathtaking views of the surrounding Sinai Mountains contrasted against crystal-clear turquoise waters. Perfect for both relaxation seekers and thrill enthusiasts, the resort offers private beach access, direct proximity to world-renowned windsurfing and scuba diving spots, authentic Bedouin-inspired hospitality, and tranquil open-air lounge areas.",
    amenities: [
      "🏖️ Private Beach",
      "🏄 Windsurfing Center",
      "📶 Free Wi-Fi",
      "🏊 Outdoor Pool",
      "🍹 Lagoon Lounge",
      "🍳 Breakfast Included"
    ],
    mapEmbedUrl: "https://maps.google.com/maps?q=Dahab%20Lagoon%20Club&t=&z=14&ie=UTF8&iwloc=&output=embed"
  },

  "elgouna": {
    name: "El Gouna Waterfront Chalet & Resort",
    location: "📍 Abu Tig Marina, El Gouna, Red Sea",
    rating: "⭐⭐⭐⭐⭐ 5-Star Lagoon View",
    price: "$200 / night",
    image: "images/Resorts/El Gouna Chalet.png",
    overview: "Discover the vibrant charm of Egypt’s premier lagoon town at El Gouna Waterfront Chalet. Surrounded by tranquil turquoise lagoons and elegant architecture, this resort offers a refined lifestyle experience. Relax on private sun decks, explore the bustling Abu Tig Marina with its fine dining and luxury boutiques, or engage in thrilling water activities like kiteboarding and sailing—all within a secure, eco-friendly coastal environment.",
    amenities: [
      "🌊 Direct Lagoon Access",
      "🛥️ Marina Proximity",
      "📶 Free High-Speed Wi-Fi",
      "🍳 Breakfast Included",
      "🏊 Private Pool Deck",
      "🚲 Bike Rentals Available"
    ],
    mapEmbedUrl: "https://maps.google.com/maps?q=Abu%20Tig%20Marina,%20El%20Gouna&t=&z=14&ie=UTF8&iwloc=&output=embed"
  },

  "soma-bay": {
    name: "Soma Bay Luxury Resort",
    location: "📍 Soma Bay Peninsula, Safaga, Red Sea",
    rating: "⭐⭐⭐⭐⭐ 5-Star Luxury",
    price: "$280 / night",
    image: "images/Resorts/Soma Bay Island.png",
    overview: "Indulge in an exclusive seaside haven situated on a private peninsula along the Red Sea. Soma Bay Luxury Resort delivers an elite vacation experience defined by pristine white-sand beaches, calm ocean breezes, and world-class leisure facilities. Guests can enjoy a championship golf course, an award-winning thalassotherapy spa, dynamic water sports activities, and luxurious suites designed to blend contemporary elegant styling with coastal comfort.",
    amenities: [
      "⛳ Championship Golf Course",
      "🧖 Spa & Thalasso",
      "🏊 Infinity Pool",
      "🛥️ Water Sports Center",
      "📶 High-Speed Wi-Fi",
      "🍷 Fine Dining"
    ],
    mapEmbedUrl: "https://maps.google.com/maps?q=Soma%20Bay,%20Red%20Sea&t=&z=13&ie=UTF8&iwloc=&output=embed"
  },

  "hurghada": {
    name: "Hurghada Long Beach Resort",
    location: "📍 Sahl Hasheesh Road, Hurghada, Red Sea",
    rating: "⭐⭐⭐⭐ 4-Star Beach Resort",
    price: "$160 / night",
    image: "images/Resorts/Hurghada Chalet.png",
    overview: "Enjoy the ultimate Red Sea family vacation along six sparkling swimming pools and a magnificent private beach stretched across the coast. Hurghada Long Beach Resort offers spacious suites with sea views, traditional Egyptian hospitality, multiple aqua parks, and diverse international dining options tailored for all ages.",
    amenities: [
      "🏖️ 1100m Private Beach",
      "🏊 6 Swimming Pools",
      "🏄 Aqua Park & Waterslides",
      "📶 Free Wi-Fi",
      "🎾 Tennis Courts",
      "🍳 All-Inclusive Dining"
    ],
    mapEmbedUrl: "https://maps.google.com/maps?q=Hurghada%20Long%20Beach%20Resort&t=&z=13&ie=UTF8&iwloc=&output=embed"
  },

  "north-coast": {
    name: "Marassi Beachfront Resort",
    location: "📍 Sidi Abdel Rahman, North Coast",
    rating: "⭐⭐⭐⭐⭐ 5-Star Mediterranean",
    price: "$350 / night",
    image: "images/Resorts/North Coast Villa.png",
    overview: "Experience Mediterranean luxury at its finest on the pristine shores of Sidi Abdel Rahman. Marassi Beachfront Resort boasts turquoise waters, powdery white sand beaches, and vibrant nightlife. Offering world-class golf courses, a bustling marina, and exquisite dining options, it stands as Egypt's ultimate summer escape for discerning travelers.",
    amenities: [
      "🏖️ Crystal Lagoon & Private Beach",
      "⛵ Luxury Yacht Marina",
      "🏊 Multiple Infinity Pools",
      "⛳ Golf Academy",
      "📶 High-Speed Wi-Fi",
      "🍳 Gourmet Breakfast"
    ],
    mapEmbedUrl: "https://maps.google.com/maps?q=Sidi%20Abdel%20Rahman,%20North%20Coast&t=&z=13&ie=UTF8&iwloc=&output=embed"
  },

  "sharm": {
    name: "Rixos Premium Resort",
    location: "📍 Nabq Bay, Sharm El Sheikh, South Sinai",
    rating: "⭐⭐⭐⭐⭐ 5-Star All-Inclusive",
    price: "$310 / night",
    image: "images/Resorts/Sharm El Sheikh.png",
    overview: "Unwind at an ultra-all-inclusive beachfront paradise overlooking the crystal-clear Gulf of Aqaba and Tiran Island. Rixos Premium Sharm El Sheikh combines opulent suites, exceptional coral reef diving spots, world-class entertainment, and a signature spa. Enjoy lavish dining choices across international restaurants and unmatched Red Sea views.",
    amenities: [
      "🤿 Private Coral Reef Jetty",
      "🍸 Ultra All-Inclusive Dining",
      "🏊 7 Outdoor Swimming Pools",
      "🧖 Anjana Luxury Spa",
      "📶 High-Speed Wi-Fi",
      "🎭 Evening Entertainment"
    ],
    mapEmbedUrl: "https://maps.google.com/maps?q=Nabq%20Bay,%20Sharm%20El%20Sheikh&t=&z=13&ie=UTF8&iwloc=&output=embed"
  }
};

    function openDetails(id) {
    localStorage.setItem('selectedResort', id);
    window.location.href = 'details.html';
}


/* ============================================
   READ & DISPLAY DETAILS IN details.html
   ============================================ */
const selectedId = localStorage.getItem('selectedResort');

if (selectedId && resortsData[selectedId]) {
    const data = resortsData[selectedId];

    const imgEl = document.getElementById("resort-img");
    const nameEl = document.getElementById("resort-name");
    const locationEl = document.getElementById("resort-location");
    const ratingEl = document.getElementById("resort-rating");
    const priceEl = document.getElementById("resort-price");
    const overviewEl = document.getElementById("resort-overview");
    const mapEl = document.getElementById("resort-map");
    const amenitiesEl = document.getElementById("resort-amenities");

    if (nameEl) {
        if (imgEl) imgEl.src = data.image;
        nameEl.innerText = data.name;
        if (locationEl) locationEl.innerText = data.location;
        if (ratingEl) ratingEl.innerText = data.rating;
        if (priceEl) priceEl.innerText = data.price;
        if (overviewEl) overviewEl.innerText = data.overview;
        if (mapEl) mapEl.src = data.mapEmbedUrl;

        if (amenitiesEl) {
            data.amenities.forEach(item => {
                const li = document.createElement("li");
                li.innerText = item;
                amenitiesEl.appendChild(li);
            });
        }
    }
}