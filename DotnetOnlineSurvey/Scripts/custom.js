(function($) {

  $(window).load(function() {
    "use strict";
    if ($.fn.blueberry) {
      $('#slider-img').blueberry();
    };
  });

  $(document).ready(function() {
    "use strict";

    $(".datepicker").datepicker( {
      changeMonth: true,
      changeYear: true,
      showButtonPanel: true,
      hideIfNoPrevNext: true,
      yearRange: '1947:+0'
    });

    $( ".tooltips" ).tooltip({ items: "img[alt]" });

    // quick search tab config
    $(".title-form").click(function() {
      var this_id = $(this).attr('id');
      $(this).parents('form').filter(':first').children('.main_form_navigation').children('.title-form').addClass('back').removeClass('current');
      $(this).addClass("current").removeClass('back');
      $(this).parents('form').filter(':first').children(".content-form").addClass("hidden");
      $("#" + this_id + "_content").removeClass('hidden');
    });

    // account navigation functions
    $("#account .faq_nav li").on('click', function() {
      var tabId = $(this).attr('id');
      $("#account #content > div").addClass("hidden"),
      $("#" + tabId + "_content").removeClass("hidden");
    });

    // myadmin advanced search toggle
    $('#advanced-search').on('click',function(e) {
      $('#advanced-search-content').toggleClass('hidden');
      e.preventDefault();
    });

    if ($.browser.msie) {
      $('.location, #sign_up_name, #sign_up_email, .form_element input, .shortcode_input, .input_placeholder').placeholder();
    }

    $(".select").selectbox();

    $(".select:disabled").selectbox("disable");

    $(".distance-range").slider({from: 0,to: 100,step: 5,dimension: '&nbsp;km'});

    $(".price_range").slider({from: 1,to: 25,step: 1,dimension: '&nbsp;Lakhs'});

    $(".details-more").css('display', 'none');

    $(".view-details").click(function() {
      $(this).css('display', 'none');
      $(this).closest('.main-block').find('.close-details').css('display', 'block');
      $(this).closest('.main-block').find('.details-more').css('display', 'block');
    });

    $(".close-details").click(function() {
      $(this).css('display', 'none');
      $(this).closest('.main-block').find('.view-details').css('display', 'block');
      $(this).closest('.main-block').find('.details-more').css('display', 'none');
    });

    $(".details div").hover(function() {
      $(this).css('color', '#EE7835');
      }, function() {
      $(this).css('color', '#378EEF');
    });

    // over lay form config
    $("#overlay_block").css('height', $(document).height());

    $(".admin-form-content").click(function(event) {
      if ($(event.target).closest(".admin-form-block").length)
        return;
      $("#overlay_block").css('display', 'none');
      $(".admin-form-content").css('display', 'none');
      event.stopPropagation();
    });
    var anc = window.location.hash.replace("#", "");
    if (anc != "") {
      Display_tab_div(anc);
    }
    $(".tab_link_button").click(function() {
      $("#overlay_block").css('display', 'block');
      var this_id = $(this).parent('span').attr('class').toLowerCase().replace(' ', '_');
      if (this_id == 'forgot_passwd') {
        $(".admin-form-content").css('display', 'none');
        $("#forgot_passwd_block").css('display', 'block');
      } else {
        $(".admin-form-content").css('display', 'none');
        $(".sign_register_block").css('display', 'block');
      }
      $('.admin-form-block .title-form').addClass('back').removeClass('current');
      $(".admin-form-block .main_form_navigation #tab_" + this_id).addClass("current").removeClass('back');
      $('.admin-form-block .content-form').addClass("hidden");
      $('.admin-form-block #tab_' + this_id + "_content").removeClass('hidden');
    });

    // overlay for blocking
    $(".overlay_block_button").on('click', function(e) {
      $("#overlay_block").css('display', 'block');
      $("#overlay_form_block").css('display', 'block');
      e.preventDefault();
    });

    // overlay for reserving
    $(".overlay_reserve_button").on('click', function(e) {
      $("#overlay_block").css('display', 'block');
      $("#overlay_form_reserve").css('display', 'block');
      e.preventDefault();
    });

    $(".faq_nav li").click(function() {
      $(".faq_nav li").removeClass('current');
      $(this).addClass('current');
    });

    $(".widget-title-sort a").click(function() {
      $(".widget-title-sort a").removeClass('current');
      $(this).addClass('current');
      $(".content-overlay").css('display', 'block').css('height', $('.product-widget > form').height()).css('width', $('.product-widget > form').width());
      $(".content-overlay > img").css('display', 'block').css('margin-top', $('.product-widget > form').height() / 2 - 33).css('margin-left', $('.product-widget > form').width() / 2 - 33);
      setTimeout(function() {
        $(".main-widget .close-details").css('display', 'none');
        $('.main-widget .view-details').css('display', 'block');
        $('.main-widget .details-more').css('display', 'none');
        $(".content-overlay").css('display', 'none');
      }, 400);
    });

    $('.content-form .return_location').css('display', 'none');
    $('.course-hidden').css('display', 'none');

    $("#course-checkbox").change(function() {
      if ($(this).is(':checked')) {
        $('.course-hidden').css('display', 'block');
      } else {
        $('.course-hidden').css('display', 'none');
      }
    });

    $("span.checkbox").live('click', function() {
      if ($(this).next('input[type="checkbox"]').attr('id') == 'course-checkbox') {
        if ($(this).next('input[type="checkbox"]').is(':checked')) {
          $('.course-hidden').css('display', 'block');
        } else {
          $('.course-hidden').css('display', 'none');
        }
      }
    });

    $("#location-checkbox").change(function() {
      if ($(this).is(':checked')) {
        $('.return_location').css('display', 'block');
      } else {
        $('.return_location').css('display', 'none');
      }
    });

    $("span.checkbox").live('click', function() {
      if ($(this).next('input[type="checkbox"]').attr('id') == 'location-checkbox' || $(this).next('input[type="checkbox"]').attr('id') == 'location-checkbox-1') {
        if ($(this).next('input[type="checkbox"]').is(':checked')) {
          $('.return_location').css('display', 'block');
        } else {
          $('.return_location').css('display', 'none');
        }
      }
    });

    $('.pagination div').click(function() {
      $('.pagination div').removeClass('current');
      $(".content-overlay").css('display', 'block').css('height', $('.product-widget > form').height()).css('width', $('.product-widget > form').width());
      $(".content-overlay > img").css('display', 'block').css('margin-top', $('.product-widget > form').height() / 2 - 33).css('margin-left', $('.product-widget > form').width() / 2 - 33);
      if ($(this).hasClass('left') || $(this).hasClass('right')) {
        if ($(this).hasClass('left')) {
          $(this).next('div').addClass('current');
        } else {
          $(this).prev('div').addClass('current');
        }
      } else {
        $(this).addClass('current');
      }
      setTimeout(function() {
        $(".main-widget .close-details").css('display', 'none');
        $('.main-widget .view-details').css('display', 'block');
        $('.main-widget .details-more').css('display', 'none');
        $(".content-overlay").css('display', 'none');
      }, 400);
    });
  });
})(jQuery);

function Display_tab_div(name) {
  (function($) {
    "use strict";
    $(".admin-form .title-form").addClass('back').removeClass('current');
    $(".admin-form #tab_" + name).addClass("current").removeClass('back');
    $(".admin-form .content-form").addClass("hidden");
    $(".admin-form #tab_" + name + "_content").removeClass('hidden');
  })(jQuery);
}
