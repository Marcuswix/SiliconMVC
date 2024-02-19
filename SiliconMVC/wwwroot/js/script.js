const toggleButton = document.getElementById("hamburger");
const menu = document.getElementById("menu-mobil");
const main = document.getElementById("main");
const footer = document.getElementById("footer");

// En "eventlyssnare" för klickhändelser på hela dokumentet
document.addEventListener('click', (event) => {
  const target = event.target;
 
  if (target !== menu && target !== toggleButton) 
  {
    if (menu.classList.contains('open')) {
      menu.classList.remove('open');
      toggleButton.classList.remove('fa-xmark');
      toggleButton.classList.add('fa-bars');
      main.classList.remove('overlay')
    }
  }
});

toggleButton.addEventListener("click", () => {
  menu.classList.toggle('open');

  if(menu.classList.contains('open')) 
  {
    toggleButton.classList.remove('fa-bars');
    toggleButton.classList.add('fa-xmark');
    main.classList.add('overlay');
    footer.classList.add('overlay');
  } 
  else 
  {
    toggleButton.classList.remove('fa-xmark');
    toggleButton.classList.add('fa-bars');
    main.classList.remove('overlay')
    footer.classList.remove('overlay')
  }
});
