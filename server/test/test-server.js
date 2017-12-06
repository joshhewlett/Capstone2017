import chai from 'chai';
import chaiHttp from 'chai-http';
import server from '../server.js';
let should = chai.should();

chai.use(chaiHttp);

describe('Blobs', function() {
    it('should return hi', function(done) {
        chai.request(server)
            .get('/hi')
            .end((err, res) => {
                res.should.have.status(200);
                done();
            });
    });
});
