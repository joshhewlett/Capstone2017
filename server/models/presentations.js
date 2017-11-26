import {
    map
} from './index';

const schema = {
    id: {
        canBeNull: false,
        type: 'number'
    },
    name: {
        canBeNull: false,
        type: 'string'
    },
    description: {
        canBeNull: true,
        type: 'string'
    },
    presented: {
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
