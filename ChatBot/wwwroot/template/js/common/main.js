﻿$().ready(function() {
	InitControl();
    setNavigation();
	$(".navbar-brand").text(document.title);
    $('#form').validate();
    $('.remove').hide();
    $(".ti-pencil-alt").attr('class', 'ti-save');
});

function InitControl() {
	$("#add").click(function(e) {
		e.preventDefault();
		if ($('#form').valid()) {
			if ($("#add").prop("name") == "POST") {
				POST();
			} else if ($("#add").prop("name") == "PUT") {
				PUT();
			}
		}
	});
	$("#clear").click(function(e) {
		e.preventDefault();
		ResetInput();
	});
}

(function($) {
	$.fn.serializeFormJSON = function() {
		var o = {};
		var a = this.serializeArray();
		$.each(a, function() {
			if (o[this.name]) {
				if (!o[this.name].push) {
					o[this.name] = "'" + [ o[this.name] ] + "'";
				}
				o[this.name].push(this.value || "'");
			} else {
				o[this.name] = this.value || "'";
			}
		});
		$(this).find("input[type = file]").each(function(i, obj) {
			let
			file = obj.files[0];
			if (file != null) {
				o[this.name] = file.name;
			} else {
				o[this.name] = "avatar1.jpg";
			}
		});
		return o;
	};
})(jQuery);

function ResetInput() {
	$('#form').each(function() {
		this.reset();
	});
	$("#add").prop("name", "POST");
	$("#add span span").text("ADD");
	$(".panel-title span").text("Add a new ... ")
	$("[id$='-error']").remove();
	$('#form').find(".error").removeClass("error");
}

var _data;
function Update(data) {

    _data = data;
    //console.log("Update");
    //console.log(data.NOTES);
    
    var VIEW_ID = data.VIEW_ID;

  //  var VIEW_ID = '163577832';

    queryReports();
    function queryReports() {
        //gapi.client.setToken('AIzaSyDUiJxZuSngmxzC-TXOLe-BmnoB31syQOc');
        gapi.client.request({
            path: '/v4/reports:batchGet',
            apiKey: 'AIzaSyDUiJxZuSngmxzC-TXOLe-BmnoB31syQOc',
            root: 'https://analyticsreporting.googleapis.com/',
            method: 'POST',
            body: {
                //apiKey:'AIzaSyDUiJxZuSngmxzC-TXOLe-BmnoB31syQOc',
                reportRequests: [
                    {
                        viewId: VIEW_ID,
                        "filtersExpression": "ga:itemRevenue!=0",
                        dateRanges: [
                            {
                                startDate: '30daysAgo',
                                endDate: 'today'
                            }
                        ],
                        metrics: [
                            {
                                "expression": "ga:itemRevenue"
                            },
                            {
                                "expression": "ga:productDetailViews"
                            },
                            {
                                "expression": "ga:quantityAddedToCart"
                            },
                            {
                                "expression": "ga:quantityCheckedOut"
                            }
                            // expression: 'ga:sessions'


                        ],
                        "dimensions": [
                            {
                                "name": "ga:productName"
                            }
                        ]
                    }
                ]
            }
        }).then(displayResults, console.error.bind(console));


        gapi.client.request({
            path: '/v4/reports:batchGet',
            apiKey: 'AIzaSyDUiJxZuSngmxzC-TXOLe-BmnoB31syQOc',
            root: 'https://analyticsreporting.googleapis.com/',
            method: 'POST',
            body: {
                //apiKey:'AIzaSyDUiJxZuSngmxzC-TXOLe-BmnoB31syQOc',
                reportRequests: [
                    {
                        viewId: VIEW_ID,
                    
                        dateRanges: [
                            {
                                startDate: '30daysAgo',
                                endDate: 'today'
                            }
                        ],
                        metrics: [
                            {
                                "expression": "ga:itemRevenue"
                            },
                            {
                                "expression": "ga:productDetailViews"
                            },
                            {
                                "expression": "ga:quantityAddedToCart"
                            },
                            {
                                "expression": "ga:quantityCheckedOut"
                            }
                            // expression: 'ga:sessions'


                        ],
                        "dimensions": [
                            {
                                "name": "ga:productListName"
                            }
                        ]
                    }
                ]
            }
        }).then(displayResults2, console.error.bind(console));


        gapi.client.request({
            path: '/v4/reports:batchGet',
            apiKey: 'AIzaSyDUiJxZuSngmxzC-TXOLe-BmnoB31syQOc',
            root: 'https://analyticsreporting.googleapis.com/',
            method: 'POST',
            body: {
                //apiKey:'AIzaSyDUiJxZuSngmxzC-TXOLe-BmnoB31syQOc',
                reportRequests: [
                    {
                        viewId: VIEW_ID,
                      
                        dateRanges: [
                            {
                                startDate: '30daysAgo',
                                endDate: 'today'
                            }
                        ],
                        metrics: [
                            {
                                "expression": "ga:pageviews"
                            },
                            {
                                "expression": "ga:pageValue"
                            },
                            {
                                "expression": "ga:timeOnPage"
                            },
                            {
                                "expression": "ga:exitRate"
                            }
                            // expression: 'ga:sessions'


                        ],
                        "dimensions": [
                            {
                                "name": "ga:pagePath"
                            }
                        ]
                    }
                ]
            }
        }).then(displayResults3, console.error.bind(console));


        gapi.client.request({
            path: '/v4/reports:batchGet',
            apiKey: 'AIzaSyDUiJxZuSngmxzC-TXOLe-BmnoB31syQOc',
            root: 'https://analyticsreporting.googleapis.com/',
            method: 'POST',
            body: {
                //apiKey:'AIzaSyDUiJxZuSngmxzC-TXOLe-BmnoB31syQOc',
                reportRequests: [
                    {
                        viewId: VIEW_ID,

                        dateRanges: [
                            {
                                startDate: '30daysAgo',
                                endDate: 'today'
                            }
                        ],
                        metrics: [
                            {
                                "expression": "ga:sessions"
                            },
                            {
                                "expression": "ga:pageviews"
                            },
                            {
                                "expression": "ga:timeOnPage"
                            },
                            {
                                "expression": "ga:transactionRevenue"
                            },
                            {
                                "expression": "ga:productAddsToCart"
                            },
                            {
                                "expression": "ga:productCheckouts"
                            },
                            {
                                "expression": "ga:productDetailViews"
                            },
                            {
                                "expression": "ga:users"
                            },
                            {
                                "expression": "ga:newUsers"
                            }
                            // expression: 'ga:sessions'


                        ],
                        "dimensions": [
                            //{
                            //    "name": "ga:pagePath"
                            //}
                        ]
                    }
                ]
            }
        }).then(displayResults4, console.error.bind(console));

        gapi.client.request({
            path: '/v4/reports:batchGet',
            apiKey: 'AIzaSyDUiJxZuSngmxzC-TXOLe-BmnoB31syQOc',
            root: 'https://analyticsreporting.googleapis.com/',
            method: 'POST',
            body: {
                //apiKey:'AIzaSyDUiJxZuSngmxzC-TXOLe-BmnoB31syQOc',
                reportRequests: [
                    {
                        viewId: VIEW_ID,

                        dateRanges: [
                            {
                                startDate: '30daysAgo',
                                endDate: 'today'
                            }
                        ],
                        metrics: [
                          
                    
                            {
                                "expression": "ga:sessions"
                            },
                            {
                                "expression": "ga:sessionDuration"
                            },
                            {
                                "expression": "ga:pageviews"
                            },
                            {
                                "expression": "ga:exits"
                            },
                            {
                                "expression": "ga:transactions"
                            },
                            {
                                "expression": "ga:transactionRevenue"
                            }
       

                        ],
                        "dimensions": [
                            {
                                "name": "ga:source"
                            }
                        ]
                    }
                ]
            }
        }).then(displayResults5, console.error.bind(console));
    }

}


function displayResults(response) {
    var data = { 'formattedJson': response.result, 'project': _data };
    var data_new = JSON.stringify(data, null, 2);
    console.log("product", data_new);
    $.ajax({
        url: "/api/OverviewEcommerceApi/PostOverviewEcommerce",
        method: "POST",
        contentType: "application/json",
        data: data_new,
     //   data: data,
        success: function (data) {
            console.log(data);
            if (data.Succeeded)
                Message("Thành công", "Bạn đã cập nhật dữ liệu Hiệu suất sản phẩm điện tử thành công!", "success");
            else
                Message("Thất bại", data.Message, "danger");
        },
        error: function () {
         //   console.log("error");
            Message("Thất bại", "Đã có lỗi xảy ra khi cập nhật dữ liệu Hiệu suất sản phẩm!", "danger");
        }
    });
}


function displayResults2(response) {
  
    var data = { 'formattedJson': response.result, 'project': _data };
    var data_new = JSON.stringify(data, null, 2);

  //  console.log("data" + JSON.stringify(data, null, 2));
    $.ajax({
        url: "/api/ProductListPerformanceEcommerceApi/PostProductListPerformanceEcommerce",
        method: "POST",
        contentType: "application/json",
        data: data_new,
        //   data: data,
        success: function (data) {
            //console.log(data);
            if (data.Succeeded)
                Message("Thành công", "Bạn đã cập nhật dữ liệu Hiệu suất doanh sách sản phẩm thành công!", "success");
            else
                Message("Thất bại", data.Message, "danger");
           
            //   $('#bootstrap-table').bootstrapTable('refresh');
            //  ResetInput();
        },
        error: function () {
          //  console.log("error");
            Message("Thất bại", "Đã có lỗi xảy ra khi cập nhật dữ liệu Hiệu suất doanh sách sản phẩm!", "danger");
        }
    });
}

function displayResults3(response) {

    var data = { 'formattedJson': response.result, 'project': _data };
    var data_new = JSON.stringify(data, null, 2);

   //   console.log("data" + JSON.stringify(data, null, 2));
    $.ajax({
        url: "/api/PageBehaviorEcommerceApi/PostPageBehaviorEcommerce",
        method: "POST",
        contentType: "application/json",
        data: data_new,
        //   data: data,
        success: function (data) {
            //console.log(data);
            if (data.Succeeded)
                Message("Thành công", "Bạn đã cập nhật dữ liệu Hiệu suất sản phẩm theo trang thành công!", "success");
            else
                Message("Thất bại", data.Message, "danger");

            //   $('#bootstrap-table').bootstrapTable('refresh');
            //  ResetInput();
        },
        error: function () {
            //  console.log("error");
            Message("Thất bại", "Đã có lỗi xảy ra khi cập nhật dữ liệu Hiệu suất sản phẩm theo trang!", "danger");
        }
    });
}



function displayResults4(response) {
    var data = { 'formattedJson': response.result, 'project': _data };
    var dataNew = JSON.stringify(data, null, 2);
 //   console.log(dataNew);
    $.ajax({
        url: "/api/OverviewEcommerceSumApi/PostOverviewEcommerce",
        method: "POST",
        contentType: "application/json",
        data: dataNew,
        //   data: data,
        success: function (data) {
         //   console.log(data);
            if (data.Succeeded)
                Message("Thành công", "Bạn đã cập nhật dữ liệu Tổng quan thương mại điện tử thành công!", "success");
            else
                Message("Thất bại", data.Message, "danger");

        },
        error: function () {
            //   console.log("error");
            Message("Thất bại", "Đã có lỗi xảy ra khi cập nhật dữ liệu Tổng quan thương mại điện tử!", "danger");
        }
    });
}


function displayResults5(response) {
    var data = { 'formattedJson': response.result, 'project': _data };
    var dataNew = JSON.stringify(data, null, 2);
    console.log("PostTrafficSourcesEcommerce"+dataNew);
    $.ajax({
        url: "/api/TrafficSourcesEcommerceApi/PostTrafficSourcesEcommerce",
        method: "POST",
        contentType: "application/json",
        data: dataNew,
        //   data: data,
        success: function (data) {
            console.log(data);
            if (data.Succeeded)
                Message("Thành công", "Bạn đã cập nhật dữ liệu Nguồn truy cập!", "success");
            else
                Message("Thất bại", data.Message, "danger");

        },
        error: function () {
            //   console.log("error");
            Message("Thất bại", "Đã có lỗi xảy ra khi cập nhật dữ liệu Nguồn truy cập!", "danger");
        }
    });
}
function Message(title, message, type) {
	$.notify({
		animate : {
			enter : 'animated bounceInDown',
			exit : 'animated bounceOutUp'
		},
		title : '<strong>' + title + '!</strong>',
		message : message
	}, {
		type : type,
		timer : 1000,
		placement : {
			from : 'top',
			align : 'center'
		}
	});
}

function setNavigation() {
    var path = window.location.pathname;
    path = path.replace(/\/$/, "");
    path = decodeURIComponent(path).substring(path.length, path.lastIndexOf("/") + 1);
    
    $(".nav a").each(function () {
        var href = $(this).attr('href');
        if (path.substring(0, href.length) === href) {
            $(this).closest('li').addClass('active');
            $(this).closest('li').parent().closest('li').addClass('active');
            $(this).closest('li').parent().closest('li').find('a').click();
        }
    });
}