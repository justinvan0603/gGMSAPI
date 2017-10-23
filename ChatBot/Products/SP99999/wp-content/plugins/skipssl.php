<?php
/*
Plugin Name: Skip SSL Verify
Plugin URI: http://www.damiencarbery.com
Description: Skip SSL verify in curl downloads - fixes: Download failed. error:0D0890A1:asn1 encoding routines:func(137):reason(161).
Author: Damien Carbery
Version: 0.1

$Id:  $
*/

function ssv_skip_ssl_verify($ssl_verify) {
    return false;
}
add_filter('https_ssl_verify', 'ssv_skip_ssl_verify');
add_filter('https_local_ssl_verify', 'ssv_skip_ssl_verify');