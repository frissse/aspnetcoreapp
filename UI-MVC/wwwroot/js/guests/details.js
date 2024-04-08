const tableEpisodes = document.getElementById("listEpisodesOfGuests");
const selectEpisodes = document.getElementById("episodeSelect");
const selectSponsors = document.getElementById("sponsorSelect");
const dateRecorded = document.getElementById("dateRecorded").value

const guestIdContainer = document.getElementById("guestId");
const guestId = parseInt(guestIdContainer.innerText);

const addEpisodeParticipationButton = document.getElementById("addEpisodeParticipationButton");

function getEpisodesOfGuests() {
    fetch(`/api/Guests/${guestId}/Episodes`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json'
        }
    })
        .then(response => { if (response.ok) return response.json(); })
        .then(data => {
            const episodes = data;
            tableEpisodes.innerHTML = '';
            episodes.forEach(episode => {
                tableEpisodes.innerHTML += `
                    <tr>
                        <td>${episode.id}</td>
                        <td>${episode.episodeTitle}</td>
                        <td>${episode.duration}</td>
                        <td>${episode.episodeNumber}</td>
                        <td>${episode.category}</td>
                        <td>${episode.listeners}</td>
                    </tr>
                `;
            });
        })
        .catch(error => console.error('Unable to get episodes.', error));
}

function getSelectionEpisodes() {
    fetch(`/api/SelectEpisodes/${guestId}`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json'
        }
    })
    .then(response => { 
        if (response.ok) {
            return response.json();
        } else {
            document.getElementById("errorResponse").innerHTML = "No episodes found";
        }
    })
    .then(data => {
        const episodes = data;
        let options = '';
        episodes.forEach(episode => {
            options += `<option value="${episode.id}">${episode.episodeTitle}</option>`;
        });
        
        selectEpisodes.innerHTML = options;
        dateRecorded.value = '0000-00-00';
    })
    .catch(error => console.error('Unable to get episodes.', error));
}

function getSponsors() {
    fetch(`/api/Sponsors/`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json'
        }
    })
    .then(response => {
        if (response.ok) {
            return response.json();
        } else {
            document.getElementById("errorResponse").innerHTML = "No sponsors found";
        }
    })
    .then(data => {
        const sponsors = data;
        let options = '';
        sponsors.forEach(sponsor => {
            options += `<option value="${sponsor.id}">${sponsor.sponsorName}</option>`;
        });
        
        selectSponsors.innerHTML = options;
    })
}

function addEpisodeParticipation(e) {
    e.preventDefault();
    const dropd = document.getElementById("episodeSelect")
    const episodeParticipation = {
        episodeId: dropd.options[dropd.selectedIndex].value,
        sponsorId: document.getElementById("sponsorSelect").value,
        guestId: guestId,
        dateRecorded: document.getElementById("dateRecorded").value
    };
    
    fetch('/api/EpisodeParticipation', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        body: JSON.stringify(episodeParticipation)
    })
    .then(response => { if (response.status === 201) {
        getEpisodesOfGuests();
        getSelectionEpisodes();
        }
        else {
            document.getElementById("errorResponse").innerHTML = "Error:" + response.status + " " + response.statusText;
        }})
        .catch(error => console.error('Unable to add episode participation.', error));
}

addEpisodeParticipationButton.addEventListener("click", addEpisodeParticipation);
getEpisodesOfGuests();
getSelectionEpisodes();
getSponsors()
