module.exports = {
    devServer: {
        proxy: {
            '/api': {
                target: 'http://localhost:26038',
                changeOrigin: true,
                logLevel: 'debug',
                secure: false,
            },
        },
        headers: {
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, PATCH, OPTIONS',
            'Access-Control-Allow-Headers': '*',
        },
    },
};
