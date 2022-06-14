(function(paymentController) {
    // var data = require("../data");
    paymentController.init = function(app) {
        app.post("/payment", (req, res) => {
            var partnerCode = "MOMO4JOK20210710";
            var accessKey = "Bk1FdDQ1xQaIIwqB";
            var secretkey = "Ehc1sAdfyL0xaGpvITrSHoenji5xOGv9";
            var requestId = partnerCode + new Date().getTime();
            var orderId = requestId;
            var orderInfo = req.body.detail;
            var redirectUrl = "https://momo.vn/return";
            var ipnUrl = "https://callback.url/notify";
            var requestType = "captureWallet";
            var extraData = req.body.id;
            var amount = req.body.payment;
            var rawSignature = "accessKey=" + accessKey + "&amount=" + amount + "&extraData=" + extraData + "&ipnUrl=" + ipnUrl + "&orderId=" + orderId + "&orderInfo=" + orderInfo + "&partnerCode=" + partnerCode + "&redirectUrl=" + redirectUrl + "&requestId=" + requestId + "&requestType=" + requestType
            const crypto = require('crypto');
            var signature = crypto.createHmac('sha256', secretkey).update(rawSignature).digest('hex');

            const requestBody = JSON.stringify({
                partnerCode: partnerCode,
                accessKey: accessKey,
                requestId: requestId,
                amount: amount,
                orderId: orderId,
                orderInfo: orderInfo,
                redirectUrl: redirectUrl,
                ipnUrl: ipnUrl,
                extraData: extraData,
                requestType: requestType,
                signature: signature,
                lang: 'en'
            });


            const options = {
                headers: {
                    'Content-Type': 'application/json',
                    'Content-Length': Buffer.byteLength(requestBody)
                }
            }
            const axios = require('axios').default;

            req = axios.post('https://test-payment.momo.vn/v2/gateway/api/create', requestBody, options)
                .then((r) => {
                    console.log(`Response: `, r);
                    console.log(`Status: ${r.status}`);
                    res.status(200).json(r.data.payUrl);
                })
        });
    };
})(module.exports);