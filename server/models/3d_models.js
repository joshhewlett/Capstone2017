import {
    map
} from './index';

const schema = {
    id: {
        canBeNull: false,
        type: 'number'
    },
    path: {
        canBeNull: false,
        type: 'string'
    },
    soft_delete: {
        canBeNull: false,
        type: 'number'
    },
    user_id: {
        canBeNull: false,
        type: 'number'
    },
};


export default class {
    constructor(json) {
        map(schema, json);
    }
};
