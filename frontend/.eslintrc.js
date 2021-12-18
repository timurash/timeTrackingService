module.exports = {
    root: true,
    env: {
        node: true,
    },
    extends: [
        'airbnb-base',
        'plugin:vue/essential',
        'plugin:vue/strongly-recommended',
        'plugin:vue/recommended',
    ],
    parserOptions: {
        parser: '@babel/eslint-parser',
    },
    rules: {
        'no-console': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
        'no-debugger': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
        'indent': ['error', 4],
        'quote-props': ['error', 'consistent'],
        'no-param-reassign': [
            'error',
            {
                props: true,
                ignorePropertyModificationsFor: [
                    'state',
                ],
            },
        ],
        'max-len': ['error', 120],
        'operator-linebreak': ['error', 'after'],
        'vue/html-indent': ['error', 4, {
            attribute: 1,
            baseIndent: 1,
            closeBracket: 0,
            alignAttributesVertically: true,
        }],
        'vue/max-attributes-per-line': ['error', {
            'singleline': 4,
            'multiline': 1,
        }],
        'vue/html-closing-bracket-newline': ['off'],
    },
    settings: {
        'import/resolver': {
            alias: {
                map: [
                    ['@', './src'],
                ],
            },
        },
    },
};
