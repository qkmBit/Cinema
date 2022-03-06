var scrolled;
var delay = 5000;
var lock = false;
var run;
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
function FPClick(){
    slideIndex=1;
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
function showSlides(n){
    var picname;
    if(n==1){
         picname="spider-man.jpg"
         $(".FirstPic").css({"background-color":"rgb(255,255,255)"})
         $(".SecondPic").css({"background-color":"rgb(255,255,255,0.4)"})
         $(".ThirdPic").css({"background-color":"rgb(255,255,255,0.4)"})
    }
    else if (n==2){
         picname="uncharted4.jpg"
         $(".FirstPic").css({"background-color":"rgb(255,255,255,0.4)"})
         $(".SecondPic").css({"background-color":"rgb(255,255,255)"})
         $(".ThirdPic").css({"background-color":"rgb(255,255,255,0.4)"})
    }     
    else if(n==3) {
        picname="kingsman.jpg"
        $(".FirstPic").css({"background-color":"rgb(255,255,255,0.4)"})
         $(".SecondPic").css({"background-color":"rgb(255,255,255,0.4)"})
         $(".ThirdPic").css({"background-color":"rgb(255,255,255)"})
    }
    else if(n>3) 
    {
        slideIndex=1;
        picname="spider-man.jpg";
        $(".FirstPic").css({"background-color":"rgb(255,255,255)"})
         $(".SecondPic").css({"background-color":"rgb(255,255,255,0.4)"})
         $(".ThirdPic").css({"background-color":"rgb(255,255,255,0.4)"})
    }
    else if(n<1){
        slideIndex=3;
        picname="kingsman.jpg";
        $(".FirstPic").css({"background-color":"rgb(255,255,255,0.4)"})
         $(".SecondPic").css({"background-color":"rgb(255,255,255,0.4)"})
         $(".ThirdPic").css({"background-color":"rgb(255,255,255)"})
    }
    var imgsrc = "img/"+picname;
    $("#afimg").attr("src",imgsrc)
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
  function showVPlayer(event){
      let div = document.getElementsByClassName("VPlayer");
      let button = document.createElement("span");
      button.classList+="CloseVP";
      button.onclick=closeVPlayer;
      let VPlayer = document.createElement("video");
      VPlayer.id="VideoPlayer";
      VPlayer.controls="controls";
      VPlayer.poster="img/spider-man.jpg";
      let src = document.createElement("source");
      src.src="video/РГР.mp4";
      src.type="video/mp4";
      VPlayer.append(src);
      div[0].append(VPlayer);
      div[0].append(button);
      $(".VPlayer").css({"visibility":"visible"});
      $("*").css({"overflow":"hidden"})
  }
  function showFilmPage(){
      $(".FilmFullPage").css({"visibility":"visible"});
  }