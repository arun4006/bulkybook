var DataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
 DataTable= $('#myTable').DataTable({
        "ajax": {
            url:"/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                          <div class="w-75 btn-group" role="group" >
                   <a href="/Admin/Product/Upsert?id=${data}"
                    class="btn btn-primary mx-3"> 
                    <i class="bi bi-pencil-square"></i> Edit
                     </a>       
                    <a class="btn btn-danger mx-3"
                      onclick="Delete('/Admin/Product/Delete/${data}')"> 
                     <i class="bi bi-trash-fill"></i> Delete
                      </a>
                      </div>   
                         
                           `
                },
                "width": "15%"
            }
        ]
 });

}

function Delete(url) {
    console.log("url" + url);
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
    if (result.isConfirmed) {
        $.ajax({
                url: url,
                type: 'DELETE',
                success: function(data) {
                    if (data.success) {
                        DataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
              }
        })
        }
    })
}