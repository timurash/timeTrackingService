module.exports = {
    devServer: {
        proxy: {
            '^/api': {
                target: 'http://localhost:26038',
                logLevel: "debug",
                secure: false,
                pathRewrite: {
                    '^/api': ''
                }
            }
        }
    }
};