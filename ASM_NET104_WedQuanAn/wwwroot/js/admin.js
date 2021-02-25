$(function () {
    'use strict';
    var $swipeTabsContainer = $('.swipe-tabs'),
        $swipeTabs = $('.swipe-tab'),
        $swipeTabsContentContainer = $('.swipe-tabs-container'),
        currentIndex = 0,
        activeTabClassName = 'active-tab';
    $swipeTabsContainer.on('init', function (event, slick) {
        $swipeTabsContentContainer.removeClass('invisible');
        $swipeTabsContainer.removeClass('invisible');
        currentIndex = slick.getCurrent();
        $swipeTabs.removeClass(activeTabClassName);
        $('.swipe-tab[data-slick-index=' + currentIndex + ']').addClass(activeTabClassName);
    });
    $swipeTabsContainer.slick({
        //slidesToShow: 3.25,
        slidesToShow: 5,
        slidesToScroll: 1,
        arrows: false,
        infinite: false,
        swipeToSlide: true,
        touchThreshold: 10
    });
    $swipeTabsContentContainer.slick({
        asNavFor: $swipeTabsContainer,
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        swipeToSlide: true,
        cssEase: 'ease',
        //easing: linear,
        //finite: false,
        //draggable: false,
        touchThreshold: 10
    });
    $swipeTabs.on('click', function (event) {
        // gets index of clicked tab
        currentIndex = $(this).data('slick-index');
        $swipeTabs.removeClass(activeTabClassName);
        $('.swipe-tab[data-slick-index=' + currentIndex + ']').addClass(activeTabClassName);
        $swipeTabsContainer.slick('slickGoTo', currentIndex);
        $swipeTabsContentContainer.slick('slickGoTo', currentIndex);


    });
    //initializes slick navigation tabs swipe handler
    $swipeTabsContentContainer.on('swipe', function (event, slick, direction) {
        currentIndex = $(this).slick('slickCurrentSlide');
        $swipeTabs.removeClass(activeTabClassName);
        $('.swipe-tab[data-slick-index=' + currentIndex + ']').addClass(activeTabClassName);
    });
});


$("#two").click(function () { $(".tab-indicator").css({ "transform": "translate3d(100%,0,0)" }); });
$("#one").click(function () { $(".tab-indicator").css({ "transform": "translate3d(0,0,0)" }); });
$("#three").click(function () { $(".tab-indicator").css({ "transform": "translate3d(200%,0,0)" }); });
$("#four").click(function () { $(".tab-indicator").css({ "transform": "translate3d(300%,0,0)" }); });
$("#five").click(function () { $(".tab-indicator").css({ "transform": "translate3d(400%,0,0)" }); });