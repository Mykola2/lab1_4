﻿$(document).ready(function () {
    refresh_api();
});

var refresh_api = function () {
    $('.api-viewer').each(function (i, viewer) {
        var route = $(viewer).attr('route');
        $('.api-table').each(function (i, elem) {
            $(elem).empty();
            $(elem).ready(function () {
                var address = route + $(elem).attr('address');
                var result = $(elem);
                $.ajax({
                    method: 'get',
                    url: address
                }).done(function (data) {
                    row = $('<tr>');
                    keys = Object.keys(data[0]).slice(1);
                    for (var j = 0; j < keys.length; j++) {
                        row.append($("<td>").text(keys[j]));
                    }
                    result.append($('<thead>').append(row));
                    tbody = $('<tbody>');
                    for (var i = 0; i < data.length; i++) {
                        var row = $('<tr>');
                        var obj = data[i];
                        row.attr('id', obj[Object.keys(data[i])[0]]);
                        keys = Object.keys(data[i]).slice(1);
                        $(row).click({ row: row, keys: keys, address: address, id: Object.keys(data[i])[0] }, change);
                        for (var j = 0; j < keys.length; j++) {
                            row.append($('<td>').text(obj[keys[j]]));
                        }
                        tbody.append(row);
                    }
                    var add_new_row = $('<tr>');
                    for (i in keys) {
                        var td = $('<td>').append(($('<input>').attr('name', keys[i])));
                        add_new_row.append(td);
                    }
                    tbody.append(add_new_row);
                    tbody.append($('<input>').attr('type', 'submit').attr('value', 'Create new').click({ keys: keys }, function (event) {
                        var keys = event.data.keys;
                        var new_obj = {};
                        for (i in keys) {
                            new_obj[keys[i]] = $(add_new_row.children('td').children('input[name="' + keys[i] + '"]')[0]).val();
                        }
                        row.attr('id',(new_obj[keys[0]]));
                        
                        $.ajax({
                            url: address,
                            type: 'PUT',
                            data: new_obj
                        }).done(function () {
                            refresh_api();
                        });
                    }));
                    result.append(tbody);
                });
            });
        });
    });
};

var change = function (event) {
    var row = event.data.row;
    var keys = event.data.keys;
    var addr = event.data.address;
    var id_name = event.data.id;
    var tds = $(row).children('td');
    for (i = 0; i < tds.length; i++) {
        var val = $(tds[i]).text();
        $(tds[i]).text('');
        var input = $('<input>').attr('type', 'text').attr('name', keys[i]).attr('value', val);
        $(tds[i]).append(input);
    }
    var change = $('<input>').attr('type', 'submit').attr('value', 'Change').click(function (event) {
        data = {};
        for (i in keys) {
            data[keys[i]] = $(row.children('td').children('input[name="' + keys[i] + '"]')[0]).val();
        }
        data[id_name] = row.attr('id');
        $.post(addr, data, function () {
            refresh_api();
        });
    });

    var del = $('<input>').attr('type', 'submit').attr('value', 'Delete').click(function (event) {

        var addr1 = addr + '/' + row[0].id;
        $.ajax({
            url: addr1,
            type: 'delete'
        }).always(function () {
            refresh_api();
        });
    });
    row.append($('<td>').append(change).append(del));
    row.off('click');
};