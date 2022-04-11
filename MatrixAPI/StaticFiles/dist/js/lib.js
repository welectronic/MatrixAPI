$(document).ready( buildMatrix);

function buildMatrix() {
  var rowA = $('#selector_rowA').val();
  var colA = $('#selector_colA').val();
  var rowB = $('#selector_rowb').val();
  var colB = $('#selector_colb').val();

  var matrixA = $('#matrixContainerA');
  var matrixB = $('#matrixContainerB');

  matrixA.empty();
  matrixB.empty();

  for (var i = 0; i < rowA; i++) {
    var row = $('<div class="row g-2"></div>');
    for (var j = 0; j < colA; j++) {
      var col = $('<div class="col-1 mb-3"><input type="number" class="form-control" id="ma_' + i + '_' + j + '"></div>');
      row.append(col);
    }
    matrixA.append(row);
  }

  for (var i = 0; i < rowB; i++) {
    var row = $('<div class="row g-2"></div>');
    for (var j = 0; j < colB; j++) {
      var col = $('<div class="col-1 mb-3"><input type="number" class="form-control" id="mb_' + i + '_' + j + '"></div>');
      row.append(col);
    }
    matrixB.append(row);
  }
}

$("[id*='selector_']").on('change', function() {
  buildMatrix();
});

$('#submit').on('click', function() {
  var rowA = $('#selector_rowA').val();
  var colA = $('#selector_colA').val();
  var rowB = $('#selector_rowb').val();
  var colB = $('#selector_colb').val();

  var matrixA = [];
  var matrixB = [];

  for (var i = 0; i < rowA; i++) {
    var row = [];
    for (var j = 0; j < colA; j++) {
      row.push($('#ma_' + i + '_' + j).val());
    }
    matrixA.push(row);
  }

  for (var i = 0; i < rowB; i++) {
    var row = [];
    for (var j = 0; j < colB; j++) {
      row.push($('#mb_' + i + '_' + j).val());
    }
    matrixB.push(row);
  }

  var data = {
    matrixA: matrixA,
    matrixB: matrixB
  };

  $.ajax({
    url: '/MatrixApi',
    type: 'POST',
      data: JSON.stringify(data),
      dataType: 'json',
      contentType: "application/json; charset=utf-8",
    success: function(response) {
      console.log(response);
        $('#result').html(JSON.stringify(response));
    }
  });
});