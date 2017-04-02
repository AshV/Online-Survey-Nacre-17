<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WelcomePage.aspx.cs" Inherits="WelcomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>



    <!--Style Sheets-->
    <link href="css/Welcomecss.css" rel="stylesheet" />
    <!--Style Sheets-->

    <!--Menu files-->
    <script src='http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js'></script>
    <link href="css/Menucss.css" rel="stylesheet" />
    <!--Menu files-->

    <!--Slideshow Starts-->
    <link href="css/Slidercssonebyone.css" rel="stylesheet" />
    <!--<script type="text/javascript" src="js/jquery.min.js"></script>-->
    <script src="js/jquery.js"></script>
    <script src="js/preloadify.js"></script>

</head>
<body style="background-image: url(images/pattern.png)" onload="noBack();"
    onpageshow="if (event.persisted) noBack();" onunload="">
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" valign="top">
                <table width="1003" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="300" align="left" valign="top" style="padding: 15px 0;"><a href="index.html">
                            <img src="images/titlelogo.png" alt="Nacre Software Pvt Ltd." height="70" border="0" style="width: 293px" /></a></td>
                        <td align="right" valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td height="25" align="right" valign="top">
                                        <table width="500" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td class="top_curve_left"></td>
                                                <td class="top_curver_border"></td>
                                                <td align="center" valign="middle" bgcolor="#fafafa" class="top_curver_bg"><a href="http://nacreservices.com/contact.html">Contact Us</a></td>
                                                <td class="top_curver_border"></td>
                                                <td align="center" valign="middle" bgcolor="#fafafa" class="top_curver_bg"><a href="LoginRegister.aspx">Login / Sign Up</a></td>
                                                <td class="top_curve_right"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div class="live_chat"><a href="#">
                    <img src="images/livechat.png" width="121" height="36" alt="Live Chat" title="Live Chat" border="0;" /></a></div>
            </td>
        </tr>
        <tr>
            <td align="center" style="background-color: #125aa5" valign="top">
                <!--Slide Show Content-->

                <!--slide show start-->

                <div class="header">

                    <div class="oneByOne1" style="overflow: hidden;">
                        <div id="onebyone_slider" style="left: -2880px;">
                            <!--  Slide 1-->

                            <div class="oneByOne_item" style="display: none; left: 0px;">
                                <span class="ob1_title animate0" style="display: none;">NACRE Survey Management<br />
                                    System</span> <span class="ob1_description animate1" style="display: none; text-align: justify; line-height: 24px;">Designed for the needs of enterprises and market research agencies, Online Survey combines elaborate and powerful functionalities to easy-to-use interfaces and tools. </span><span class="ob1_button animate2" style="display: none;"><a href="#" class="default_button">Read more</a></span>
                                <img src="images/slideone.jpg" class="ob1_img_device1 animate3" alt="" style="display: none; padding-left: 50px;" />
                            </div>
                            <!--  Slide 1-->
                            <!--  Slide 2-->
                            <div class="oneByOne_item" style="display: none; left: 0px;"><span class="ob1_title animate0" style="display: none; font-size: 28px;">Create questionnaires with point and
                                <br />
                                click ease</span> <span class="ob1_description animate1" style="display: none; text-align: justify; line-height: 24px;">This is an all in one<strong>Online Survey Management System</strong> service designed for people who are not computer experts and have the need to conduct surveys by themselves.</span> <span class="ob1_button animate2" style="display: none; margin-top: -2px;"><a href="#" class="default_button">Read more</a></span>
                                <img src="images/slidetwo.jpg" class="ob1_img_device1 animate3" alt="" style="display: none; padding: 30px 0 0 150px;" />
                            </div>

                            <!--  Slide 2-->

                            <!--  Slide 3-->
                            <div class="oneByOne_item" style="display: none; left: 0px;"><span class="ob1_title animate0" style="display: none;">Distributing to Your Audience</span> <span class="ob1_description animate1" style="display: none; text-align: justify; line-height: 24px;">Simply choose between emailing your customers automatically through our system, sharing survey which we will provide in any way you like (facebook, Gmail, internal email etc.)  </span><span class="ob1_button animate2" style="display: none;"><a href="#" class="default_button">Read more</a></span>
                                <img src="images/slidethree.jpg" class="ob1_img_device1 animate3" alt="" style="display: none; padding-left: 120px; margin-top: -20px;" />
                            </div>
                            <!--  Slide 3-->

                            <!--  Slide 4-->
                            <div class="oneByOne_item" style="display: none; left: 960px;">
                                <span class="ob1_title animate0" style="display: none;">Incredible Reporting </span><span class="ob1_description animate1" style="display: none; text-align: justify; line-height: 24px;">When you want to impress a client, influence your boss’ decision, or just make sense of all of the great market data that your online survey collected, our proprietary reporting system is sure to make your job easy.
                                    <p>
                                        Thanks to the innovative reporting system, you can turn thousands of rows of online survey data into a colorful chart with the push of a button.
                                    </p>
                                </span><span class="ob1_button animate2" style="display: none; visibility: hidden;"><a href="#" class="default_button">Read more</a></span>
                                <img src="images/slidefour.png" class="ob1_img_device1 animate3" alt="" style="display: none;" />
                            </div>
                            <!--  Slide 4-->
                            <!--  Slide 5-->
                            <div class="oneByOne_item" style="display: block; left: 2880px;"><span class="ob1_title animate0 fadeInRight" style="display: block;">Versatility</span> <span class="ob1_description animate1 fadeInRight" style="display: block; text-align: justify; line-height: 24px;">Sometimes a textual questionnaire isn’t good enough! Thanks to our impressive questionnaire maker, you can easily add images,  fill-in-the-blank, or multiple choice options to your online survey and then rebrand that survey with your own logo. In addition, there are plenty of online survey designs to choose from!</span> <span class="ob1_button animate2 fadeInRight" style="display: block;"><a href="#" class="default_button">Read more</a></span>
                                <img src="images/slidefive.jpg" class="ob1_img_device1 animate3 fadeInRight" alt="" style="display: block;" />
                            </div>
                            <!--  Slide 5-->
                            <!--  Slide 6-->

                            <div class="oneByOne_item" style="display: none; left: 960px;"><span class="ob1_title animate0" style="display: none;">Secure Data</span> <span class="ob1_description animate1" style="display: none; text-align: justify; line-height: 24px;">You can sleep soundly at night knowing the data you collect in each free online survey belongs entirely to you and will remain confidential. We are 100% committed to respecting and protecting your privacy at all times!<strong> (Full Privacy Policy Here)</strong></span> <span class="ob1_button animate2" style="display: none;"><a href="#" class="default_button">Read more</a></span>
                                <img src="images/slidesix.jpg" class="ob1_img_device1 animate3" alt="" style="display: none;" />
                            </div>

                            <!--  Slide 6-->

                            <!--  Slide 7-->
                            <div class="oneByOne_item" style="display: none; left: 1920px;"><span class="ob1_title animate0" style="display: none;">How many web companies<br />
                                are built  customer surveys?</span> <span class="ob1_description animate1" style="display: none; text-align: justify; line-height: 24px;">Almost all major companies survey their customers and use the results to help determine best-practices as well as staff bonuses and pay rises. </span><span class="ob1_button animate2" style="display: none;"><a href="kclink_web_hosting_and_online_marketing.html" class="default_button">Read more</a></span>
                                <img src="images/slideseven.png" class="ob1_img_device1 animate3" alt="" style="display: none; margin-top: -20px;">
                            </div>

                            <!--  Slide 7-->

                        </div>
                        <div class="buttonArea">
                            <div class="buttonCon" style="cursor: pointer; display: block; opacity: 0.93624375;"><a class="theButton" rel="0">1</a><a class="theButton" rel="1">2</a><a class="theButton" rel="2">3</a><a class="theButton active" rel="3">4</a></div>
                        </div>
                        <div class="arrowButton" style="display: block; cursor: pointer; opacity: 0.93624375;">
                            <div class="prevArrow"></div>
                            <div class="nextArrow"></div>
                        </div>
                    </div>
                    <!-- END OF: #onebyone_slider -->

                </div>

                <!--slideshow ends-->

                <!--Slide Show Content-->

            </td>
        </tr>

        <tr>
            <td height="250" align="center" valign="top" id="content_bg">
                <table width="1003" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="25"></td>
                    </tr>
                    <tr>
                        <td>
                            <table width="1003" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="center" valign="top" class="service_box">
                                        <table width="238" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td height="10"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top"><a href="#">
                                                    <img src="images/who-we-are.png" title="Who We Are" alt="Who Wew Are" width="100" height="70" border="0" /></a></td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top" class="services_heading">Who We Are</td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="middle">
                                                    <div class="services_button"><a href="http://www.nacreservices.com/who.html">Read more</a></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="17"></td>
                                    <td align="center" valign="top" class="service_box">
                                        <table width="238" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td height="10"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top"><a href="kclink_mobile_application_development.html">
                                                    <img src="images/what-we-do-icon.png" title="What We Do" width="100" height="70" border="0" /></a></td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top" class="services_heading">
                                                What We Do
                                            </tr>
                                            <tr>
                                                <td align="center" valign="middle">
                                                    <div class="services_button"><a href="http://www.nacreservices.com/what.html">Read more</a></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="17"></td>
                                    <td align="center" valign="top" class="service_box">
                                        <table width="238" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td height="10"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top"><a href="kclink_website_design_and_development.html">
                                                    <img src="images/how-we-do.png" title="How we do" width="100" height="70" border="0" /></a></td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top" class="services_heading">How We Do </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="middle">
                                                    <div class="services_button"><a href="http://www.nacreservices.com/how.html">Read more</a></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="17"></td>
                                    <td align="center" valign="top" class="service_box">
                                        <table width="238" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td height="10"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top"><a href="kclink_web_hosting_and_online_marketing.html">
                                                    <img src="images/contactus.gif" title="contact us" width="100" height="70" border="0" /></a></td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top" class="services_heading">To Know More About Us</td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="middle">
                                                    <div class="services_button"><a href="http://nacreservices.com/">Read more</a></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="25"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" valign="top">
                <table width="1003" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="20"></td>
                    </tr>
                    <tr>
                        <td height="180" align="left" valign="top">
                            <table width="1003" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="left" valign="top">
                                        <div class="welcome_heading" style="margin: 0 0 3px 0;"><strong style="color: #3e3e3e;">Welcome to</strong> Nacre Software Services Pvt. Ltd.</div>
                                        <div class="welcome_text">
                                            NACRE Software Services, a dedicated division of Naresh I Technologies, has been initiated for this purpose.
            <p>
                It is here that individuals undergo wholesome mentoring and nurturing programme aimed to equip them with the Employability Skills to meet the industry requirements. Continuous feedback and absorption from the Industry is our key metric for success of our program.
                                            </p>
                                        </div>
                                        <div class="style_button" style="float: right; margin-top: -8px;"><a href="about_kclink.html">Read More</a></div>
                                    </td>
                                    <td width="30">&nbsp;</td>
                                    <td width="300" height="180" align="right" valign="top"><a href="#">
                                        <img src="images/nacrelogo.jpg" alt="Nacre" width="300" height="180" border="0" /></a></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="30">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" id="footer_top_line"></td>
        </tr>
        <tr>
            <td height="120" align="center" valign="top" bgcolor="#3a3a3a">
                <table width="1003" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="20"></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <table width="1003" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td valign="top" align="center">
                                        <div class="footer_links">
                                            <%--<a href="index.html">Home</a> &nbsp; | &nbsp; <a href="about_kclink.html">About Us</a> &nbsp; | &nbsp; <a href="#">Services</a> &nbsp; | &nbsp; <a href="kclink_automotive.html">Industries</a> &nbsp; | &nbsp; <a href="kclink_airport_management_system.html">Solutions</a> &nbsp; | &nbsp; <a href="kclink_banking_suite.html">Products</a> &nbsp; | &nbsp; <a href="kclink_careers.html">Careers</a> &nbsp; | &nbsp; <a href="kclink_clients.html">  Clients</a> &nbsp; | &nbsp; <a href="kclink_contactus.html">Contact Us</a> </div>--%>

                                            <div class="footer_links" style="margin: 20px 0 0 0;">Copyright &copy; @2014, <strong>Nacre Software Services Pvt. Ltd.</strong></div>
                                    </td>
                                    <td width="30">&nbsp;</td>
                                    <td width="250" align="left" valign="top">&nbsp;

            
            
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <!--Slideshow Starts-->
    <script type="text/javascript" src="js/iview.js"></script>
    <script type="text/javascript" src="js/raphael.js"></script>
    <script type="text/javascript" src="js/jquery.plugins.js"></script>
    <script type="text/javascript" src="js/bottom.js"></script>
    <!--Slideshow Ends-->

    <%--<!--Scroll Top Files-->
	<!-- easing plugin ( optional ) -->
	<script src="js/easing.js" type="text/javascript"></script>
	<!-- UItoTop plugin -->
	<script src="js/jquery.ui.totop.js" type="text/javascript"></script>
	<!-- Starting the plugin -->
	<script type="text/javascript">
	    $(document).ready(function () {
	        /*
			var defaults = {
	  			containerID: 'toTop', // fading element id
				containerHoverID: 'toTopHover', // fading element hover id
				scrollSpeed: 1200,
				easingType: 'linear' 
	 		};
			*/

	        $().UItoTop({ easingType: 'easeOutQuart' });

	    });
	</script>
<!--Scroll Top Files-->--%>
</body>
</html>
