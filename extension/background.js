
browser.tabs.onUpdated.addListener(async (tabId, changeInfo, tab) => {
  try {
    await browser.scripting.executeScript({
      target: {
        tabId: tab.id,
        allFrames: true,
      },
      files: ["/content.js"],
    });
  } catch (err) {
    console.error(`failed to execute script: ${err}`);
  }
});
