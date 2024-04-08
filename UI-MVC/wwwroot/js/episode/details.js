const listeners = document.getElementById('listeners');
const editButton = document.getElementById('editButton');

const episodeIdContainer = document.getElementById('episodeId');
const episodeId = parseInt(episodeIdContainer.innerText);

function editListeners(e) {
    const newListeners = parseInt(listeners.value);
    const data = { 
        episodeId: episodeId, 
        listeners: newListeners 
    };
    fetch(`/api/Episodes/${episodeId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => { 
            if (response.status === 204) {
                getEpisode();
                document.getElementById("editFeedback").innerHTML = "<p>Episode updated successfully</p>";
        } else {
            document.getElementById("editFeedback").innerHTML = "Error:" + response.status + " " + response.statusText;
        }})
        .catch(error => console.error('Unable to edit episode.', error));
}

function getEpisode() {
    fetch(`/api/Episodes/${episodeId}`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json'
        }
    })
    .then(response => {
        if (response.ok) {
            return response.json();
        }
    })
    .catch(error => console.error('Unable to get episode.', error));
}
editButton.addEventListener('click', editListeners);