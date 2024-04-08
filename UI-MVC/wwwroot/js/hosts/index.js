const spinner = document.getElementById('spinner');
const reloadHostsButton = document.getElementById('reloadHosts')
const errorMessage = document.getElementById('errorMessage')

function showSpinner() {
    spinner.classList.add('show');
}

function removeSpinner() {
    spinner.classList.remove('show');
}

const sleep = (ms) => {
    return new Promise((resolve) => setTimeout(resolve, ms));
};

const loadHosts = async () => {
    eraseList()
    try {
        const response = await fetch('/api/Hosts/', {
            signal: AbortSignal.timeout( 5000 ),
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }})
        if (response.status !== 200) {
            errorList()
        }
        const data = await response.json()
        await sleep(2000)
        await showAllHosts(data)
    } catch (e) {
        console.error(e);
    }
    
}

const reloadHosts = async () => {
    eraseList()
    await loadHosts()
}

function errorList() {
    let hostTable = document.getElementById("tableHosts")
    let html = `<table class="table">
        <thead>
            <tr>
                <th>Id</th>
                <th>Name</th>
                <th>Year first published</th>
                <th>Rating</th>
                <th>Gender</th>
            </tr>
        </thead>
        <tbody>
        <tr>
            <td>Something</td>
            <td>went</td>
            <td>wrong</td>
            <td>try</td>
            <td>again</td>
        </tr>
        </tbody></table>`
    hostTable.innerHTML = html;
}

function eraseList() {
    let hostTable = document.getElementById("tableHosts")
    let html = `<table class="table">
        <thead>
            <tr>
                <th>Id</th>
                <th>Name</th>
                <th>Year first published</th>
                <th>Rating</th>
                <th>Gender</th>
            </tr>
        </thead>
        <tbody>
        <tr>
            <td>Hosts</td>
            <td>are</td>
            <td>loading</td>
            <td>please</td>
            <td>wait</td>
        </tr>
        </tbody></table>`
    hostTable.innerHTML = html;
}

function showAllHosts(responses) {
    let hostTable = document.getElementById("tableHosts")
    let html = `<table class="table">
        <thead>
            <tr>
                <th>Id</th>
                <th>Name</th>
                <th>Year first published</th>
                <th>Rating</th>
                <th>Gender</th>
            </tr>
        </thead>
        <tbody>`
       
        for (const r of responses) {
            html += `
                <tr>
                    <td id="hostId">${r.id}</td>
                    <td>${r.name}</td>
                    <td>${r.yearFirstPublished}</td>
                    <td>${r.rating}</td>
                    <td>${r.gender}</td>
                </tr>`
        }

        html += `</tbody></table>`
        hostTable.innerHTML = html;
        
}

loadHosts();
reloadHostsButton.addEventListener("click", reloadHosts);