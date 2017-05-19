function initializeRoutes(){
	var $routes_group = {};
	
	jQuery.ajax({
		type: "GET",
		url: "http://bustrackerweb.azurewebsites.net/api/Route/GetRoutes",
		headers: {          
			Accept: "application/xml"  
		},
		dataType: "text",
		success: function (data) {
			var xmlDoc = $.parseXML(data),
				$xml = $(xmlDoc);
			var $routes = $xml.find('RouteModel');
			
			$(".routes-badge").text($routes.length);
						
			var stops = {};
			
			$routes.each(function () {
				var routename = $(this).find('RouteName').text();
				var index = routename.indexOf('-');
				if(routename[index + 1] == ' ') {
					routename = routename.substring(0, index != -1 ? index : routename.length);
					routename = routename.trim();
				}
				if(!stops[routename]) {
					stops[routename] = true;
				}
			});
			
			for(var stop in stops) {
				var $route_stops = [];
				
				$routes.each(function () {
					var routename = $(this).find('RouteName').text();
					var index = routename.indexOf('-');
					if(routename[index + 1] == ' ') {
						routename = routename.substring(0, index != -1 ? index : routename.length);
						routename = routename.trim();
					}
										
					if(stop == routename) {
						$route_stops.push($(this));
					}
				});
				$routes_group[stop] = $route_stops;
			};
			
			$.each($routes_group, function(route_name, routes) {
				$(".routes-group-list").append(
					"<a href=\"javascript:void(0)\" class=\"list-group-item justify-content-between\"><span class=\"badge badge-default badge-pill\">"
					+ routes.length
					+ "</span>"
					+ route_name
					+ "</a>"
				);
			});
		}
	});
	
	$('#searchroutes').keyup(function(){
		var current_query = $('#searchroutes').val();
		if (current_query !== "") {
			$(".routes-group-list .list-group-item").hide();
			$(".routes-group-list .list-group-item").each(function(){
				var current_keyword = $(this).clone().children().remove().end().text();
				if (current_keyword.indexOf(current_query) >=0) {
					$(this).show();    	 	
				};
			});   	
		} else {
			$(".routes-group-list .list-group-item").show();
		};
	});
	
	$('body').on('click', '.routes-group-list a', function() {
		var stationName = $(this).clone().children().remove().end().text();
		
		$("#routeModal .route-name").text(stationName);
		var $routesavailable = $routes_group[stationName];
		console.log($($routesavailable[0]).find("RouteName").text());
		$(".routes-list").empty();
		$.each($routesavailable, function(key, value) {
			$(".routes-list").append(
				"<a href=\"javascript:void(0)\" class=\"list-group-item justify-content-between\">"
				+ "Route Id: " + $(value).find("RouteId").text() + "<br>"
				+ "Route Name: " + $(value).find("RouteName").text() + "<br>"
				+ "Route Number: " + $(value).find("RouteNumber").text() + "<br>"
				+ "Route Type: " + $(value).find("RouteType").text() + "<br>"
				+ "</a>"
			);
		});
		
		$('#routeModal').modal("show");
	});
};