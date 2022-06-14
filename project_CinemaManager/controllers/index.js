(function(controllers) {
    const homeController = require("./homeController");
    const paymentController = require("./paymentController");
    controllers.init = function(app) {
        homeController.init(app);
        paymentController.init(app);
    };
})(module.exports);