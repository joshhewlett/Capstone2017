import passport from 'passport';
import GooglePassport from 'passport-google-oauth';

const GoogleStrategy = GooglePassport.OAuth2Strategy;

export default (app) => {

    app.passport = passport;

    passport.serializeUser((user, cb) => {
        cb(null, user.id);
    });

    passport.deserializeUser((id, cb) => {
        app.models.user.findById(id).then(user => {
            cb(null, user);
        }).catch((err) => {
            cb(err, null);
        });
    });

    passport.use(new GoogleStrategy({
        clientID: process.env.AUTH_CLIENT_ID,
        clientSecret: process.env.AUTH_CLIENT_SECRET,
        callbackURL: process.env.AUTH_CALLBACK_URL
    }, (accessToken, refreshToken, profile, done) => {
        app.models.user.findOne({
            where: {
                email: profile.emails[0].value
            }
        }).then((user) => {
            if (user) {
                return done(null, user);
            } else {
                app.models.user.create({
                    email: profile.emails[0].value
                }).then((data) => {
                    app.logger.debug("New user created", data);
                    done(null, data);
                }).catch((err) => {
                    app.logger.warn("Failed to create user", err);
                    done(err);
                });
            }
        }).catch((err) => {
            app.logger.warn("Google login error", err);
            done(err);
        });
    }));

    app.use(passport.initialize());
    app.use(passport.session());
};
