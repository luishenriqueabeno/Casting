jQuery(document).ready(function () {
	jQuery('.cursosuperior').click(function() {
	  jQuery('#cursosuperiorid').fadeIn(200);
	  jQuery('#cursotecnicoid').fadeOut(200);
	  jQuery('#cursosuperiorid2').fadeOut(200);
	});
});

jQuery(document).ready(function () {
	jQuery('.cursosuperior2').click(function() {
	  jQuery('#cursosuperiorid2').fadeIn(200);
	  jQuery('#cursotecnicoid').fadeOut(200);
	  jQuery('#cursosuperiorid').fadeOut(200);
	});
});

jQuery(document).ready(function () {
	jQuery('.cursotecnico').click(function() {
	  jQuery('#cursotecnicoid').fadeIn(200);
	  jQuery('#cursosuperiorid').fadeOut(200);
	  	  jQuery('#cursosuperiorid2').fadeOut(200);
	});
});

jQuery(document).ready(function () {
	jQuery('.nehsuperior').click(function() {
	  jQuery('#cursosuperiorid').fadeOut(200);
	  jQuery('#cursotecnicoid').fadeOut(200);
	  	  jQuery('#cursosuperiorid2').fadeOut(200);
	});
});

jQuery(document).ready(function () {
	jQuery('#tem_carro').click(function() {
	  jQuery('#tipo_carro').fadeIn(200);
	});
	jQuery('#tem_filho').click(function() {
	  jQuery('#qnts_filhos').fadeIn(200);
	});

	jQuery('#nao_tem_carro').click(function() {
	  jQuery('#tipo_carro').fadeOut(200);
	});
	jQuery('#nao_tem_filho').click(function() {
	  jQuery('#qnts_filhos').fadeOut(200);
	});
});


function redirect() {
window.location.href = 'home.html';
}

function redirectdigitador() {
window.location.href = 'home_digitador.html';
}

function redirectadmin() {
window.location.href = 'home_adm.html';
}



jQuery(document).ready(function () {
jQuery('#redirect').click(function() {
var redirecionar = window.setTimeout(redirect,5000);
});
});


jQuery(document).ready(function () {
jQuery('#redirectdigitador').click(function() {
var redirecionar2 = window.setTimeout(redirectdigitador,5000);
});
});

jQuery(document).ready(function () {
jQuery('#redirectadmin').click(function() {
var redirecionar3 = window.setTimeout(redirectadmin,5000);
});
});


jQuery(document).ready(function () {
    jQuery('#possuiAparelho').click(function () {
        jQuery('.retirada').fadeIn(200);
    });

    jQuery('#naoPossuiAparelho').click(function () {
        jQuery('.retirada').fadeOut(200);
    });
});		

jQuery(document).ready(function () {
    jQuery('#PossuiCicatriz').click(function () {
	    jQuery('#localcicatriz').fadeToggle(200);
	});
});		

jQuery(document).ready(function () {
    jQuery('#PossuiPiercing').click(function () {
	  jQuery('#piercingarea').fadeToggle(200);
	});
    jQuery('#PossuiTatuagem').click(function () {
	 jQuery('#tatuagemarea').fadeToggle(200);
	});
});		

jQuery(document).ready(function () {
	jQuery('#apenddar').click(function() {
	jQuery(".experiencia").clone().attr('class','exp_nova').appendTo(".bloco_experiencia");
	});
});		


jQuery(document).ready(function () {
	jQuery('#up_arquivo').click(function() {
	  jQuery('#fotoarquivo').show(200);
	  jQuery('#fotourl').hide(200);
	});
	jQuery('#up_url').click(function() {
	 jQuery('#fotourl').show(200);
	 jQuery('#fotoarquivo').hide(200);
	});
});		
