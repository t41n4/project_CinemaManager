(function(database) {
    const Connection = require("tedious").Connection;

    const config = {
        userName: "sqlserver",
        password: "Hien9029",
        server: "35.220.208.39",
        options: {
            database: "qlrp",
            encrypt: true,
            rowCollectionOnDone: true
        }
    };

    database.getDirectory = function(next) {
        const connection = new Connection(config);
        connection.on("connect", function(err) {
            if (err) {
                next(err, null);
            } else {
                console.log("Connected to database");
                const Request = require("tedious").Request;
                const queryString = "Select * from TaiKhoan"
                const request = new Request(queryString, function(err, rowCount, rows) {
                    if (err) {
                        next(err, null);
                    }
                }).on("doneInProc", function(rowCount, more, rows) {
                    const jsonArray = [];
                    rows.forEach(function(columns) {
                        const rowObject = {};
                        columns.forEach(function(column) {
                            rowObject[column.metadata.colName] = column.value;
                        });

                        jsonArray.push(rowObject);
                    });

                    next(null, jsonArray);
                });

                connection.execSql(request);
            }
        });
    }
})(module.exports);