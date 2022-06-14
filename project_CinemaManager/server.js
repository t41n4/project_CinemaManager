var http = require("http");
var express = require("express");
var app = express();
var bodyParser = require('body-parser')
app.use(bodyParser.urlencoded({ extended: false }))
app.use(bodyParser.json())
    // Set vash view engine.
app.set("view engine", "vash");

app.use(express.static(__dirname + "/public"));

var controllers = require("./controllers");
controllers.init(app);

var server = http.createServer(app);
const port = process.env.PORT || 3000;
server.listen(port);