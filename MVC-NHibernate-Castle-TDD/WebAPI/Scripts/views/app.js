function insertUpdate() {

    swal({
        title: "",
        text: "Tem certeza que deseja realizar essa operação?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        confirmButtonText: "Sim",
        cancelButtonText: "Não",
        showLoaderOnConfirm: true,
    }, function () {

        var Id = $("#Id").val();
        var Nome = $("#Nome").val();
        var Telefone = $("#Telefone").val();
        var Endereco = $("#Endereco").val();

        $.ajax({
            type: "POST",
            url: "/api/api/AddEdit?Id=" + Id + "&Nome=" + Nome + "&Telefone=" + Telefone + "&Endereco=" + Endereco,
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                //alert(response.Id);
                swal({
                    title: "",
                    text: "Operação realizada com sucesso!",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonText: "OK",
                    closeOnConfirm: false,
                    allowEscapeKey: false
                }, function () {
                    window.location.href = "/Cliente";
                });
            },
            error: function (response) {
                swal({
                    title: "",
                    text: "Erro ao inserir cliente!",
                    type: "error"
                });
            }
        });

    });

};

function deleteCliente(id) {
    swal({
        title: "",
        text: "Tem certeza que deseja realizar essa operação?",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        confirmButtonText: "Sim",
        cancelButtonText: "Não",
        showLoaderOnConfirm: true,
    }, function () {

        $.ajax({
            type: "POST",
            url: "/api/api/Delete?Id=" + id,
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                //alert(response.Id);
                swal({
                    title: "",
                    text: "Operação realizada com sucesso!",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonText: "OK",
                    closeOnConfirm: false,
                    allowEscapeKey: false
                }, function () {
                    window.location.href = "/Cliente";
                });
            },
            error: function (response) {
                swal({
                    title: "",
                    text: "Erro ao deletar cliente!",
                    type: "error"
                });
            }
        });

    });
};