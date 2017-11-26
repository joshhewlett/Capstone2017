import {
    map
} from './index';

const schema = {
    id: {
        canBeNull: false,
        type: 'number'
    },
    email: {
        canBeNull: false,
        type: 'string'
    }
};


export default class {
    constructor(json) {
        map(schema, json);
    }
};
