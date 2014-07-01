jQuery(document).ready(function() {
		/* initialize the external events */
		jQuery('#external-events div.external-event').each(function() {
		
			// create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
			// it doesn't need to have a start or end
			var eventObject = {
				title: jQuery.trim(jQuery(this).text()) // use the element's text as the event title
			};
			
			// store the Event Object in the DOM element so we can get to it later
			jQuery(this).data('eventObject', eventObject);
			
			// make the event draggable using jQuery UI
			jQuery(this).draggable({
				zIndex: 999,
				revert: true,      // will cause the event to go back to its
				revertDuration: 0  //  original position after the drag
			});
			
		});

		
});
