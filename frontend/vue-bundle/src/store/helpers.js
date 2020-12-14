import { lodash as _ } from '@/utils/lodash.js';

export function SET_STATE(context, data) {
    if (Array.isArray(data)) {
        data.map(({ state, payload }) => (context[state] = payload));
    } else if (!Object.keys(data).includes('state')) {
        Object.entries(data).forEach(([state, value]) => _.set(context, state, value));
    } else {
        const { state, payload } = data;
        context[state] = payload;
    }
}