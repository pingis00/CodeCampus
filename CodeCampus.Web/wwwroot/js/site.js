const toggleMenu = () => {
    console.log('Toggling menu');
    document.getElementById('menu').classList.toggle('hide');
    document.getElementById('account-buttons').classList.toggle('hide');
}

const checkScreenSize = () => {
    console.log('Checking screen size');
    if (window.innerWidth >= 1200) {
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
}

window.addEventListener('resize', checkScreenSize);
checkScreenSize();

function initMap() {
    var centerCoords = { lat: 37.733905, lng: -122.406190 }
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
    let sw = document.querySelector('#switch-mode')

    sw.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light"

        fetch(`/sitesettings/changetheme?mode=${theme}`)
            .then(res => {
                if (res.ok)
                    window.location.reload()
                else
                    console.log('something')
            })
    })
})