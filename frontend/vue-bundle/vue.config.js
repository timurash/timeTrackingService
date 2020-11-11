module.exports = {
    devServer: {
        proxy: {
            '^/api': {
                target: 'https://localhost:26038',
                logLevel: "debug",
                secure: false,
                pathRewrite: {
                    '^/api': ''
                }
            }
        }
    }
};