// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(".cep").mask("00.000-000");
});

$(document).ready(function () {

    function limpa_formulario_cep() {
        $("#Estado").val("");
        $("#Cidade").val("");
        $("#Endereco").val("");
        $("#Bairro").val("");
        $("#Complemento").val("");
    }
    // perder foco CEP
    $("#CEP").blur(function () {
        var cep = $(this).val().replace(/\D/g, '');

        // verifica valor cep

        if (cep != "") {
            //regular expression
            var validacep = /^[0-9]{8}$/;

            // valida formato
            if (validacep.test(cep)) {

                $("#Estado").val("...");
                $("#Cidade").val("...");
                $("#Logradouro").val("...");
                $("#Bairro").val("...");
                $("#Complemento").val("...");

                $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                    if (!("erro" in dados)) {
                        // atualiza os campos com valores da consulta
                        $("#Estado").val(dados.uf);
                        $("#Cidade").val(dados.localidade);
                        $("#Logradouro").val(dados.logradouro);
                        $("#Bairro").val(dados.bairro);
                        $("#Complemento").val(dados.complemento);
                    }
                    else {
                        // cep pesquisado n existe
                        limpa_formulario_cep();
                        alert("CEP não foi encontrado.");
                    }

                })
            }
            else {
                // cep inváliado
                limpa_formulario_cep();
                alert("Formato de CEP inválido.");
            }
        }
        else {
            //cep sem valor
            limpa_formulario_cep();
        }
    })
})


