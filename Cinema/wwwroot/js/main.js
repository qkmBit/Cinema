var scrolled;
var delay = 5000;
var lock = false;
var run;
let ticketList = [];
window.addEventListener('resize', setEqualHeight);
window.addEventListener('load',setEqualHeight)
function setEqualHeight(){
    var wwidth= document.documentElement.clientWidth
    var height = wwidth*0.76/4;
    $(".afisha").css({"height":height});
    $("#afimg").css({"height":height});
}
window.onscroll = function() {
    scrolled = window.pageYOffset || document.documentElement.scrollTop;
    if(scrolled > 50){
        $("header").css({"background":"rgb(0, 0, 0)"})
        $(".main-menu").css({"background-color":"rgb(0,0,0)"})
    }
    if(50 > scrolled){
        $("header").css({"background": "rgb(73, 73, 73,0.4)"})
        $(".main-menu").css({"background-color":"rgb(73,73,73,0.4)"})     
    }
}
var slideIndex = 1;
function plusSlides(n) {
    showSlides(slideIndex += n);
  }
function FPClick(n){
    slideIndex=n;
    showSlides(slideIndex)
}
function SPClick(){
    slideIndex=2;
    showSlides(slideIndex)
}
function TPClick(){
    slideIndex=3;
    showSlides(slideIndex)
}
function showSlides(n) {
    var arrDots = Array.from(document.getElementById("picCount").children);
    var arrPost = Array.from(document.getElementById("img").children);
    for (var i = 0; i < arrPost.length; i++) {
        arrPost[i].style.display = "none";
        arrDots[i].style.backgroundColor = "rgb(255,255,255,0.4)";
    }
    if (n > arrPost.length - 1) {
        slideIndex = 0;
        arrPost[0].style.display = "block";
        arrDots[0].style.backgroundColor = "rgb(255,255,255)";
    }
    else if (n < 0) {
        arrPost[arrPost.length - 1].style.display = "block";
        arrDots[arrDots.length-1].style.backgroundColor = "rgb(255,255,255)";
        slideIndex = arrPost.length - 1;
    }
    else {
        arrPost[n].style.display = "block";
        arrDots[n].style.backgroundColor = "rgb(255,255,255)";
    }
  }
  function auto(){
    if (lock == true) {
        lock = false;
        window.clearInterval(run);
       }
       else if (lock == false) {
        lock = true;
        run = setInterval("plusSlides(1)", delay);
      }
  }
  function closeVPlayer(event){
      let parent = event.currentTarget.parentElement;
      while(parent.firstChild){
          parent.removeChild(parent.firstChild);
      }
      $(".VPlayer").css({"visibility":"hidden"});
      $("*").css({"overflow":"visible"});
      $(".main-menu").css({"overflow":"hidden"});

  }
  function showVPlayer(id){
      $.get('/Home/VPlayer/' + id , function (data) {
          $('.VPlayer').html(data);
      });
      $(".VPlayer").css({"visibility":"visible"});
      $("*").css({"overflow":"hidden"})
  }
  function showFilmPage(id){
      $.get('/Home/FilmFullPage/' + id, function (data) {
          $('.FilmFullPage').html(data);
      });
      $(".FilmFullPage").css({ "visibility": "visible" });
      $("*").css({ "overflow": "hidden" })
}
function closeFilmPage(event) {

    let parent = event.currentTarget.parentElement;
    while (parent.firstChild) {
        parent.removeChild(parent.firstChild);
    }
    $(".FilmFullPage").css({ "visibility": "hidden" });
    $("*").css({ "overflow": "visible" });
    $(".main-menu").css({ "overflow": "hidden" });
}
function showTicketList(id) {
    $.get('/Home/SessionTicketList/' + id, function (data) {
        $('.SessionTicketList').html(data);
    });
    $("*").css({ "overflow": "hidden" })
    $(".SessionTicketList").css({ "visibility": "visible" });
}
function closeTicketPage(event) {

    let parent = event.currentTarget.parentElement;
    while (parent.firstChild) {
        parent.removeChild(parent.firstChild);
    }
    ticketList.length = 0;
    $(".SessionTicketList").css({ "visibility": "hidden" });
    $("*").css({ "overflow": "visible" });
    $(".main-menu").css({ "overflow": "hidden" });
}
function seatClick(event) {
    let checkbox = event.currentTarget.querySelector(".free");
    let row = event.currentTarget.parentElement.parentElement.id;
    let number = event.currentTarget.querySelector(".seatNum").innerText;
    let curOrder = document.getElementsByClassName("tickets");
    checkbox.checked = !checkbox.checked;
    if (checkbox.checked) {
        if (curOrder[0].childElementCount < 7) {
            event.currentTarget.style.backgroundColor = "yellow";
            let ticket = document.createElement("span");
            ticket.innerHTML = row + "/" + number;
            ticket.className = "ticket";
            curOrder[0].append(ticket);
            ticketList.push(event.currentTarget.id);
        }
    }
    else {
        let tickStr = row + "/" + number;
        event.currentTarget.style.backgroundColor = "orange";
        ticketList.splice(ticketList.indexOf(event.currentTarget.id), 1);
        for (var i = 0; i < curOrder[0].childNodes.length; i++) {
            if (curOrder[0].childNodes[i].innerHTML == tickStr) {
                curOrder[0].removeChild(curOrder[0].childNodes[i]);
            }
        }
    }
    document.getElementsByClassName("orderSumRes")[0].innerHTML=ticketList.length*300 + " P."
}
function buyTickets(event) {
    let order = {
        tickets: ticketList
    };
    let json = JSON.stringify(order);
    $.ajax({
        url: "Home/Buy/",
        type: 'Post',
        data: {
            tickets: ticketList
        },
        success: function (data) {
            alert(data.result);
        }
    });
}