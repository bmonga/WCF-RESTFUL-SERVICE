
var baseurl = "http://localhost:52875/BookService.svc";
var regex = /^[0-9]*$/;
$(function () {
   // calls delete
    $('#btnDeleteBook').click(function () {
        var bookID = $("#txtBookID").val();
        if ((bookID.length != 0) && (regex.test(bookID))) {
            $.ajax({
                type: "DELETE",
                url:  baseurl + "/book/delete/" + bookID,
                ContentType: 'json',
                dataType: 'json',
                crossDomain: true,
                success: function (data, status, jqXHR) {                    
                    message("Book Data deleted successfully");                  
                },

                error: function (xhr) {
                    message(xhr.responseText);
                }
            });
        }
        else {
           
            message("Wrong Input : BookId is either blank or is not a number");
        }

    });
    //gets the book with the given Book ID
    $('#btnGetBookDetails').click(function () {
        var bookID = $("#txtBookID").val();
        if ((bookID.length != 0) && (regex.test(bookID))) {
            $.ajax({
                type: "GET",
                url: baseurl + "/book/" + bookID,
                ContentType: 'json',
                dataType: 'json',
                crossDomain: true,
                success: function (data) {
                    var result = data.GetBookResult;


                    var id = result.Id;
                    var Title = result.Title;
                    var Description = result.Description;
                    $('#txtAddBookID').val(id);
                    $('#txtAddBookTitle').val(Title);
                    $('#txtAddBookDescription').val(Description);




                },

                error: function (xhr) {
                    message(xhr.responseText);
                }
            });
        }
        else {
            message("Wrong Input : BookId is either blank or is not a number");
        }

        });
    
    // add the new book
    $('#btnAddNewBook').click(function () {
       

        var checkId = $("#txtAddBookID").val();
        if ((checkId.length != 0) && (regex.test(checkId))) {
            var BookData = {
                book: {
                    Id: $("#txtAddBookID").val(),
                    Title: $("#txtAddBookTitle").val(),
                    Description: $("#txtAddBookDescription").val()

                }
            };

            $.ajax({
                type: "POST",
                url: baseurl + "/Book/Add",
                data: JSON.stringify(BookData),

                contentType: 'application/json; charset=utf-8',
                crossDomain: true,
                dataType: 'json',

                success: function (data, status, jqXHR) {
                    message("Book added successfully");
                },
                error: function (xhr) {
                    message(xhr.responseText);
                   
                }
            });
        }
        else {
            
            message("Wrong Input : BookId is either blank or is not a number");
            
        }

    });
    // update the book
    $('#btnUpdateBook').click(function () {
        var checkId = $("#txtAddBookID").val();
        if ((checkId.length != 0) && (regex.test(checkId))) {
            var BookData = {
                book: {
                    Id: $("#txtAddBookID").val(),
                    Title: $("#txtAddBookTitle").val(),
                    Description: $("#txtAddBookDescription").val()

                }
            };

            $.ajax({
                type: "PUT",
                url: baseurl + "/Book/Update",
                data: JSON.stringify(BookData),

                contentType: 'application/json; charset=utf-8',
                crossDomain: true,
                dataType: 'json',

                success: function (data, status, jqXHR) {
                    message("Book updated successfully");
                },
                error: function (xhr) {
                   
                    message(xhr.responseText);
                }
            });
        }
        else {
            message("Wrong Input : BookId is either blank or is not a number");
        }


    });


});

// creates a dialog for the error message
function message(msg) {
  //  var msg = "Wrong Input : BookId is either blank or is not a number";
    document.getElementById("msg").innerHTML = msg;
    $(function () {
        $("#dialog").dialog();
    });
};
// loads all the books
function auto_load() {
    $('#tbDetails tbody').remove();
    $.ajax({
        type: "GET",
        url: baseurl + "/books",
        ContentType: 'json',
        dataType: 'json',
        crossDomain: true,
        success: function (data) {
            var result = data.GetAllBooksResult;
            for (i = 0; i < result.length; i++) {
                var id = result[i].Id;
                var Title = result[i].Title;
                var Description = result[i].Description;
                $('<tr><td>' + id + '</td><td>' + Title + '</td><td>' + Description + '</td><td>').appendTo('#tbDetails');
            }
            $('#tbDetails').show();
        },
        error: function (xhr) {

            alert('Service call failed: ' + xhr.responseText);
        }
    });

};


$(document).ready(function () {

    auto_load(); //Call auto_load() function when DOM is Ready

});

// refresh data periodically
setInterval(auto_load, 10000);
