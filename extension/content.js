function addCrossToUsernames() {
  const commentContainers = document.querySelectorAll('.ytd-comment-thread-renderer');
  commentContainers.forEach(container => {
    const usernameElement = container.querySelector(" .style-scope .ytd-comment-view-model .style-scope .ytd-comment-view-model > a")
    if (usernameElement) addCrossToUsername(usernameElement);
  });
}

function blurFilteredComments(){
  const commentContainers = document.querySelectorAll('.ytd-comment-thread-renderer');
  commentContainers.forEach(container => {
    const usernameElement = container.querySelector(" .style-scope .ytd-comment-view-model .style-scope .ytd-comment-view-model > a")
    if (usernameElement &&  BlockList.includes(usernameElement.textContent.trim())) {
     container.style.filter = 'blur(10px)'
    }
  });
}

function addCrossToUsername(usernameElement) {
  const crossElement = document.createElement('span');
  crossElement.className = 'cross-icon';
  crossElement.innerHTML = '❌';

  if (usernameElement.classList.contains('cross-added')) return;

  usernameElement.parentElement.append(crossElement);

  usernameElement.classList.add('cross-added');

  crossElement.addEventListener('click', (event) => {
    event.stopPropagation();
    const username = usernameElement.textContent.trim();
    // Send the username to the API using an HTTP request
    fetch('http://127.0.0.1:5035/api/BlockListApi', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: `"${username}"`
    })
    .then(response => {
      
      console.log('Username sent to API successfully:', response);
      BlockList.push(username);
      blurFilteredComments();

    })
    .catch(error => {
      console.error('Error sending username to API:', error);
    });

  });
}

let BlockList;

function run() {
  const observer = new MutationObserver(mutations => {
    mutations.forEach(mutation => {
      if (mutation.addedNodes.length > 0) {
        mutation.addedNodes.forEach(node => {
          if (node.classList && node.classList.contains('ytd-comment-thread-renderer')) {
            addCrossToUsernames();
            blurFilteredComments();
          }
        });
      }
    });
  });
  
  observer.observe(document.body, { childList: true, subtree: true });
}

function fetchData() {
  fetch('http://127.0.0.1:5035/api/BlockListApi')
  .then(response => response.json())
  .then(data => {
    // Process the fetched data
    BlockList = data.map(e=> e.name);
    run()
  })
  .catch(error => {
    console.error('Error fetching data:', error);
  });
}

fetchData();
