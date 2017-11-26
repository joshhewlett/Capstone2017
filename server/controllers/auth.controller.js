import BaseController from './base.controller';

export default class extends BaseController {
    constructor(app) {
        super(app);

        this.router.get('/login', app.passport.authenticate('google', {
            scope: ['profile', 'email']
        }));

        this.router.get('/google/callback',
            app.passport.authenticate('google', {
                failureRedirect: '/auth/fail'
            }), (req, res) => {
                this.success(req, res);
            });

        this.router.get('/fail', (req, res) => {
            this.fail(req, res);
        });

        this.router.get('/logout', (req, res) => {
            this.logout(req, res);
        });
    }

    /**
     * Destorys the session, logging the user out
     * @param {Object} req 
     * @param {Object} res 
     */
    async logout(req, res) {
        this.app.logger.debug("User is logging out");
        req.logout();
        req.session.destroy();
        this.sendResponse(res, "Logged out");
    }

    /**
     * Gets called after successful google login
     * @param {Object} req 
     * @param {Object} res 
     */
    async success(req, res) {
        this.sendResponse(res, "Logged in");
    }

    /**
     * Gets called after failed google login
     * @param {Object} req 
     * @param {Object} res 
     */
    async fail(req, res) {
        this.sendResponse(res, "Failed to login", this.HttpStatus.UNAUTHORIZED);
    }
};
