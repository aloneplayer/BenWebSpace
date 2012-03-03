/* Copyright 2008 by MSE-iT / Thomas Maierhofer www.maierhofer.de
 * Licenced under: LGPL http://www.gnu.org/licenses/lgpl.html
 *
 */



(function($) {
    $.fn.backgroundCanvas = function() {
        $(this).each
 	    (function() {

 	        var $this = $(this);

 	        // JQuery Browser version fix for IE6 & IE7 detection
 	        $.browser.version = $.browser.msie && parseInt($.browser.version) == 6 && window["XMLHttpRequest"] ? "7.0" : $.browser.version;

 	        // Remove background and border color, that canvas can bee seen.
 	        $this.css("background-color", "transparent");
 	        $this.css("border-color", "transparent");
 	        $this.css("background-image", "none");

 	        $this.wrapInner('<div class="jbgContentDiv" style="width:auto; height:auto; border: 0px transparent solid; margin: 0 0 0 0; display:block; position:relative;"><\/div>');
 	        var $content = $this.children(".jbgContentDiv");

 	        if ($.browser.msie) 
 	        {
			
				if( ! window.XMLHttpRequest )
				{
					// Hopefully nobody will use this color
		 	        $this.css("border-color", "#fac2f5");
		 	        $this.css("filter","chroma(color=#fac2f5)");
		 	        
		 	        if( ! $this.get(0).hasLayout )
		 	        {
		 	        	// Forces a layout to the element so the filter can apply
			 	        $this.css("zoom","1");
		 	        }
				}
				
 	            var divElement = document.createElement('div');
 	            divElement.className = "jbgCanvasDiv";
 	            divElement.style.position = "relative";
 	            divElement.style.display = "block";
 	            divElement.style.height = "0px";
 	            divElement.style.width = "0px";

 	            var canvasElement = document.createElement('canvas');
 	            canvasElement.className = "jbgCanvas";
 	            canvasElement.style.height = "0px";
 	            canvasElement.style.width = "0px";
 	            canvasElement.style.position = "absolute";

 	            canvasElement = G_vmlCanvasManager.initElement(canvasElement);
 	            $this.get(0).insertBefore(divElement, $this.get(0).firstChild);
 	            divElement.appendChild(canvasElement);
 	        }
 	        else {
 	            $this.prepend('<div class="jbgCanvasDiv" style="display:block; position:relative;'
    	 	    + ' width:0px; height:0px; padding: 0 0 0 0; margin: 0 0 0 0;">'
    	 	    + '<canvas class="jbgCanvas" style="position:absolute; width:0px; height:0px;" ></canvas></div>');
 	        }

 	    });

        return this;
    };


    $.fn.backgroundCanvasPaint = function(paintFkt) {
        $(this).each
 	    (function() {
 	        var $this = $(this);
 	        var $canvasDiv = $this.children(".jbgCanvasDiv");
 	        var $canvas = $canvasDiv.children(".jbgCanvas");
 	        var $content = $this.children(".jbgContentDiv");
 	        if ($canvas.length == 0)
 	            return this;

 	        var canvas = $canvas.get(0);
 	        var width = $this.outerWidth();
 	        var height = $this.outerHeight();

 	        $canvas.width(width + '.4px');
 	        $canvas.height(height + '.4px');


 	        // the padding must be transfered to the content DIV
 	        var paddingTop = parseFloat( $this.css("padding-top"));
 	        var paddingBottom = parseFloat( $this.css("padding-bottom"));
 	        var paddingLeft = parseFloat( $this.css("padding-left"));
 	        var paddingRight = parseFloat( $this.css("padding-right"));


 	        // The borders are needed to offset the canvas object 	        
 	        var borderTop = $this.css("border-top-width");
 	        if ($this.css("border-top-style") == "none")
 	            borderTop = "0";

 	        var borderBottom = $this.css("border-bottom-width");
 	        if ($this.css("border-bottom-style") == "none")
 	            borderBottom = "0";


 	        var borderLeft = $this.css("border-left-width");
 	        if ($this.css("border-left-style") == "none")
 	            borderLeft = "0";

 	        var borderRight = $this.css("border-right-width");
 	        if ($this.css("border-right-style") == "none")
 	            borderRight = "0";
			
			
 	        if ($.browser.msie) 
 	        {
 	        	// Adjust Boder symbolic values to the numeric values they express
 	            switch ( borderTop  ) {
 	                case "thin": borderTop = "2"; break;
 	                case "medium": borderTop = "4"; break;
 	                case "thick": borderTop = "6"; break;
 	            }
 	            
 	            switch ( borderBottom ) {
 	                case "thin": borderBottom = "2"; break;
 	                case "medium": borderBottom = "4"; break;
 	                case "thick": borderBottom = "6"; break;
 	            }
 	            
 	            
 	            switch ( borderLeft  ) {
 	                case "thin": borderLeft = "2"; break;
 	                case "medium": borderLeft = "4"; break;
 	                case "thick": borderLeft = "6"; break;
 	            }
 	        
 	            switch ( borderRight ) {
 	                case "thin": borderRight = "2"; break;
 	                case "medium": borderRight = "4"; break;
 	                case "thick": borderRight = "6"; break;
 	            }
 	        
 	        	// Adjust IECanvas internal DIV
 	            $canvas.children("div").width(width + '.4px');
 	            $canvas.children("div").height(height + '.4px');
 	        
 	        }

			// We need the values to calculate something 
			borderTop = parseFloat( borderTop );
			borderBottom = parseFloat( borderBottom );
			borderLeft = parseFloat( borderLeft );			
			borderRight = parseFloat( borderRight );

			// Adjust the Canvas according to the parameters
			$canvasDiv.css("top", -(borderTop + paddingTop ) + "px");
			$canvasDiv.css("left", -(borderLeft + paddingLeft ) + "px");

 	        if (canvas.getContext) {
 	            canvas.width = width;
 	            canvas.height = height;

 	            var ctx = canvas.getContext("2d");
                var elementInfo= { 
                		canvas: canvas, 
                		$canvas: $canvas, 
                		$canvasDiv:$canvasDiv, 
                		$content:$content, 
                		$this:$this, 
                		
                		borderLeft: borderLeft,
                		borderRight: borderRight,
                		borderTop: borderTop,
                		borderBottom: borderBottom,
                		
                		paddingTop: paddingTop,
                		paddingBottom: paddingBottom,
                		paddingLeft: paddingLeft,
                		paddingRight: paddingRight
                		
                		
                		};
                
 	            paintFkt(ctx, width, height, elementInfo );

 	        }
 	        else {
 	            alert("can't create context")
 	        }



 	    });

        return this;
    };



    // Helper functions to paint background elements like rounded rects on canvas
    $.canvasPaint = {
        roundedRect: function(ctx, options) {
            options = jQuery.extend({
                width: 0,
                radius: 0,
                border: 0,
                stroke: false,
                fill: true,
                adjustRadius: true
            }, options);

            options = jQuery.extend({
                x: 0,
                y: 0,
                height: options.width,
                radiusTL: options.radius,
                radiusTR: options.radius,
                radiusBL: options.radius,
                radiusBR: options.radius,
                borderL: options.border,
                borderR: options.border,
                borderT: options.border,
                borderB: options.border
            }, options);

            /*
            radius = Math.min(radius,height/2);
            radius = Math.min(radius,width/2);
            */



            if (options.adjustRadius) {
                options.radiusTL = Math.max(options.radiusTL - ((options.borderT + options.borderL) / 2), 0);
                options.radiusTR = Math.max(options.radiusTR - ((options.borderT + options.borderR) / 2), 0);
                options.radiusBL = Math.max(options.radiusBL - ((options.borderB + options.borderL) / 2), 0);
                options.radiusBR = Math.max(options.radiusBR - ((options.borderB + options.borderR) / 2), 0);

            }


            var x = options.x + options.borderL;
            var y = options.y + options.borderT;
            var width = options.width - options.borderL - options.borderR;
            var height = options.height - options.borderT - options.borderB;




            var kappaRradiusTL = options.radiusTL * 0.3333;
            var kappaRradiusTR = options.radiusTR * 0.3333;
            var kappaRradiusBL = options.radiusBL * 0.3333;
            var kappaRradiusBR = options.radiusBR * 0.3333;

            ctx.beginPath();
            ctx.moveTo(x, y + options.radiusTL);              // left top corner
            ctx.lineTo(x, y + height - options.radiusBL);        // left line to bottom  corner

            ctx.bezierCurveTo(x, y + height - kappaRradiusBL, x + kappaRradiusBL, y + height, x + options.radiusBL, y + height); // left lower corner

            ctx.lineTo(x + width - options.radiusBR, y + height); // lower line to right lower corner

            ctx.bezierCurveTo(x + width - kappaRradiusBR, y + height, x + width, y + height - kappaRradiusBR, x + width, y + height - options.radiusBR); // right lower corner

            ctx.lineTo(x + width, y + options.radiusTR); // right line to upper corner

            ctx.bezierCurveTo(x + width, y + kappaRradiusTR, x + width - kappaRradiusTR, y, x + width - options.radiusTR, y); // right upper corner

            ctx.lineTo(x + options.radiusTL, y); // top line to left corner

            ctx.bezierCurveTo(x + kappaRradiusTL, y, x, y + kappaRradiusTL, x, y + options.radiusTL); // left upper corner

            if (options.stroke)
                ctx.stroke();

            if (options.fill)
                ctx.fill();
        },


        // draws a tab with round corners 
        roundTab: function(ctx, options) {
            options = jQuery.extend({
                x: 0,
                y: 0,
                width: 0,
                radiusLeft: 0,
                radiusRight: 0,
                bottomRadiusLeft: 0,
                bottomRadiusRight: 0,
                offsetLeft: 0,
                offsetRight: 0,
                border: 0,
                stroke: false,
                fill: true,
                adjustRadius: true
            }, options);

            options = jQuery.extend({
                height: options.width
            }, options);

            /*
            radius = Math.min(radius,height/2);
            radius = Math.min(radius,width/2);
            */

            if (options.adjustRadius) {
                options.radiusLeft = options.radiusLeft - options.border;
                options.radiusRight = options.radiusRight - options.border;
                options.bottomRadiusLeft = options.bottomRadiusLeft - options.border;
                options.bottomRadiusRight = options.bottomRadiusRight - options.border;
            }

            var x = options.x + options.border;
            var y = options.y + options.border;
            var width = options.width - options.border - options.border;
            var height = options.height - options.border;





            var hypoLeft = Math.sqrt((height * height) + (options.offsetLeft * options.offsetLeft));
            var hypoRight = Math.sqrt((height * height) + (options.offsetRight * options.offsetRight));


            ctx.beginPath();
            // Connection Point calc.
            // ctx.moveTo(x,y+height); // Lower right corner

            var xRadius = (options.radiusLeft * options.offsetLeft) / hypoLeft;
            var yRadius = (options.radiusLeft * height) / hypoLeft;
            var kappaRadius = options.radiusLeft * 0.3333;
            var kappaXRadius = xRadius * 0.3333;
            var kappaYRadius = yRadius * 0.3333;
            ctx.moveTo(x + options.offsetLeft - xRadius, y + yRadius); // left line

            ctx.bezierCurveTo(x + options.offsetLeft - kappaXRadius, y + kappaYRadius, x + options.offsetLeft + kappaRadius, y, x + options.offsetLeft + options.radiusLeft, y); // Left upper radius


            var xRadius = (options.radiusRight * options.offsetRight) / hypoRight;
            var yRadius = (options.radiusRight * height) / hypoRight;
            var kappaRadius = options.radiusRight * 0.3333;
            var kappaXRadius = xRadius * 0.3333;
            var kappaYRadius = yRadius * 0.3333;

            ctx.lineTo(x + width - options.offsetRight - options.radiusRight, y); // top line       

            ctx.bezierCurveTo(x + width - options.offsetRight - kappaRadius, y,
                x + width - options.offsetRight + kappaXRadius, y + kappaYRadius,
                x + width - options.offsetRight + xRadius, y + yRadius); // Right upper radius 




            var xRadius = (options.bottomRadiusRight * options.offsetRight) / hypoRight;
            var yRadius = (options.bottomRadiusRight * height) / hypoRight;
            var kappaRadius = options.bottomRadiusRight * 0.3333;
            var kappaXRadius = xRadius * 0.3333;
            var kappaYRadius = yRadius * 0.3333;

            ctx.lineTo(x + width - xRadius, y + height - yRadius); // right line
            ctx.bezierCurveTo(x + width - kappaXRadius, y + height - kappaYRadius,
                x + width + kappaRadius, y + height,
                x + width + options.bottomRadiusRight, y + height); // right lower radius

            var xRadius = (options.bottomRadiusLeft * options.offsetLeft) / hypoLeft;
            var yRadius = (options.bottomRadiusLeft * height) / hypoLeft;
            var kappaRadius = options.bottomRadiusLeft * 0.3333;
            var kappaXRadius = xRadius * 0.3333;
            var kappaYRadius = yRadius * 0.3333;


            ctx.lineTo(x - options.bottomRadiusLeft, y + height); // Lower right corner               

            ctx.bezierCurveTo(x - kappaRadius, y + height,
                x + kappaXRadius, y + height - kappaYRadius,
                x + xRadius, y + height - yRadius); // left lower radius

            if (options.stroke)
                ctx.stroke();

            if (options.fill)
                ctx.fill();
        }


    };

})(jQuery)