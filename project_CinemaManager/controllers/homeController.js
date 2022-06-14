(function(homeController) {
    homeController.init = function(app) {
        app.get("/", function(req, res) {

            res.status(200).json("This is G9 Cinema Manager API");
        });
    };
})(module.exports);