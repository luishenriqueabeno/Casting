jQuery(document).ready(function () {
    jQuery('#enviar_senha').click(function () {
        jQuery('#img1').hide();
        jQuery('#text1').hide();
        jQuery('#img2').show();
        jQuery('#text2').show();
    });
    jQuery('#email').click(function () {
        jQuery('#error').show();
    });


    jQuery('#enviar_cadastro').click(function () {
        var senha = jQuery('#senha').val();
        var confirmacao = jQuery('#confsenha').val();
        var cpf = jQuery('#cpf').val();
        var nome = jQuery('#userName').val();

        if (cpf == "" || nome == "" || senha == "") {
            jQuery('#senhaErrada').hide();
            jQuery('#senhaMenor').hide();
            jQuery('#campoNaoPreenchido').slideDown('fast');
        } else if (senha.length < 6) {
            jQuery('#campoNaoPreenchido').hide();
            jQuery('#senhaErrada').hide();
            jQuery('#senhaMenor').slideDown('fast');
        }
        else if (confirmacao != senha) {
            jQuery('#senhaMenor').hide();
            jQuery('#campoNaoPreenchido').hide();
            jQuery('#senhaErrada').slideDown('fast');
        } else {
            jQuery(this).hide();
            jQuery('#campoNaoPreenchido').hide();
            jQuery('#senhaErrada').hide();
            jQuery('#senhaMenor').hide();
            jQuery('.formUsuarioPost').submit();
        }


    });

});