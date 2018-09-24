const VERIFICATION_SERVER_URL = 'https://localhost:44383/';
const FIREBASE_KEY_PATH = './firebase-config.json';

// Initializa Firebase App...
function initApp() {
    // Initialize Firebase
    $.getJSON(FIREBASE_KEY_PATH).then(configData => {
        firebase.initializeApp(configData);

        firebase.auth().onAuthStateChanged(user => {
            if (user) {
                logStatus('User is signed in...');
                logStatus(user);
                $('#sign-out-button').css('display', 'flex');
                $('#sign-in-buttons').css('display', 'none');
            } else {
                logStatus('No user is signed in...');
                $('#sign-out-button').css('display', 'none');
                $('#sign-in-buttons').css('display', 'flex');
            }
        });
    });
}

// Event Handlers...
function signInOnClick() {
    logStatus('Signing in with google...');
    let provider = new firebase.auth.GoogleAuthProvider();

    firebase
        .auth()
        .signInWithPopup(provider)
        .then(result => {
            logStatus("Sign in successful.");
            logStatus(result);
        })
        .catch(error => {
            logStatus('ERROR Signing In...');
            logStatus(error);
        });
}

function signOutOnClick() {
    logStatus("Signing Out...");

    firebase
        .auth()
        .signOut()
        .then(() => {
            logStatus("Successfully signed out...");
        })
        .catch(error => {
            logStatus("Error signing out... See console...");
        });
}

function checkTokenOnClick() {
    firebase
        .auth()
        .currentUser
        .getIdToken(true)
        .then(token => {
            logStatus(`Checking Token: ${token}`);

            try {
                $.ajax({
                    'url': `${VERIFICATION_SERVER_URL}api/TestFirebaseAuth`,
                    'headers': {
                        "Authorization": `Bearer ${token}`,
                        "Access-Control-Allow-Headers": "*"
                    },
                    'success': result => {
                        logStatus(result);
                    },
                    'complete': (jqXHR, status) => {
                        logStatus(jqXHR);
                        logStatus(status);
                    }
                });
            } catch (e) {
                logStatus(e);
            }
        });
}

function clearStatusLogOnClick() {
    $('#status-log').html('');
}

function healthCheckOnClick() {
    try {
        $.ajax({
            'url': `${VERIFICATION_SERVER_URL}/HealthCheck`,
            'success': result => {
                logStatus(result);
            },
            'complete': (jqXHR, status) => {
                logStatus(jqXHR);
                logStatus(status);
            }
        });
    } catch (e) {
        logStatus(e);
    }
}

// Utilitites...
function logStatus(status) {
    let initialHTML = $('#status-log').html();

    let today = new Date();
    let date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
    let time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
    let dateTime = date + ' ' + time;

    if (typeof status == 'object') {
        status = objectToTextarea(status);
    }

    $('#status-log')
        .html(`${initialHTML} <br />${dateTime}: ${status}`)
        .animate({
            scrollTop: $('#status-log').prop("scrollHeight")
        }, 10);
}

function objectToTextarea(obj) {
    return `<textarea class="code">${JSON.stringify(obj, null, 4)}</textarea>`;
}