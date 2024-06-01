const toggleMenu = () => {
    const header = document.querySelector('header');
    const isMobile = window.innerWidth < 1200;
    const body = document.body;
    if (isMobile) {
        header.classList.toggle('full-screen');
        body.classList.toggle('no-scroll');
    }
    document.getElementById('menu').classList.toggle('hide');
    document.getElementById('account-buttons').classList.toggle('hide');
    document.querySelector('.btn-switch').classList.toggle('hide');
}

const checkScreenSize = () => {
    const header = document.querySelector('header');
    const body = document.body;
    if (window.innerWidth >= 1200) {
        header.classList.remove('full-screen');
        body.classList.remove('no-scroll');
        document.getElementById('menu').classList.remove('hide');
        document.getElementById('account-buttons').classList.remove('hide');

    } else {
        if (!document.getElementById('menu').classList.contains('hide')) {
            document.getElementById('menu').classList.add('hide');
        }
        if (!document.getElementById('account-buttons').classList.contains('hide')) {
            document.getElementById('account-buttons').classList.add('hide');
        }
    }
    if (window.innerWidth >= 992) {
        document.querySelector('.btn-switch').classList.remove('hide');
    } else {
        if (!document.querySelector('.btn-switch').classList.contains('hide')) {
            document.querySelector('.btn-switch').classList.add('hide');
        }
    }
}

window.addEventListener('resize', checkScreenSize);
checkScreenSize();

function initMap() {
    var centerCoords = { lat: 37.733905, lng: -122.406190 };
    var mapOptions = {
        center: centerCoords,
        zoom: 15,
    };
    var map = new google.maps.Map(document.getElementById('map'), mapOptions);

    var marker1 = new google.maps.Marker({
        position: centerCoords,
        map: map,
        title: 'Centered marker',
        icon: '/Assets/Icons/ui/marker-icon.svg'
    });

    var offsetCoords = { lat: 37.729743, lng: -122.410866 };

    var marker2 = new google.maps.Marker({
        position: offsetCoords,
        map: map,
        title: 'Another marker',
        icon: '/Assets/Icons/ui/marker-icon.svg'
    });
}

document.addEventListener('DOMContentLoaded', function () {
    let sw = document.querySelector('#switch-mode');

    sw.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light"

        fetch(`/sitesettings/changetheme?mode=${theme}`)
            .then(res => {
                if (res.ok)
                    window.location.reload()
                else
                    console.log('something')
            });
    });
});

function handleFormSubmit(formId, messageContainerId) {
    const form = document.getElementById(formId);
    const messageContainer = document.getElementById(messageContainerId);

    if (!form || !messageContainer) {
        console.error(`Form or message container not found for ID: ${formId} / ${messageContainerId}`);
        return;
    }

    form.addEventListener('submit', function (event) {
        event.preventDefault();
        const formData = new FormData(form);

        fetch(form.action, {
            method: form.method,
            body: formData,
        })
            .then(response => response.json())
            .then(data => {
                console.log(`Response for ${formId}: `, data);
                displayMessage(messageContainer, data, form);
            })
            .catch(error => {
                console.error(`Error for ${formId}: `, error);
            });
    });
}

function displayMessage(messageContainer, data, form) {
    messageContainer.innerHTML = data.message;
    messageContainer.classList.add('fade-out');
    messageContainer.classList.remove('hide');

    if (data.success) {
        messageContainer.classList.add('alert-success');
        messageContainer.classList.remove('alert-danger');
        form.reset();
    } else {
        messageContainer.classList.add('alert-danger');
        messageContainer.classList.remove('alert-success');
    }

    setTimeout(() => {
        messageContainer.classList.add('hide');
    }, 3000);

    messageContainer.addEventListener('transitionend', () => {
        messageContainer.innerHTML = '';
        messageContainer.classList.remove('alert-danger', 'alert-success', 'fade-out', 'hide');
    }, { once: true });
}

handleFormSubmit('newsletterForm', 'newsletter-message-container');
handleFormSubmit('contactForm', 'contact-message-container');