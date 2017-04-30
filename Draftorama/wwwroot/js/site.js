$("#text-draftbot-intro-1").delay(500).animate({ opacity: 1 }, 400);
$("#text-draftbot-intro-2").delay(2500).animate({ opacity: 1 }, 400);
$("#buttons-draftbot-intro-1").delay(3000).animate({ opacity: 1 }, 400);
$("#buttons-draftbot-intro-2").delay(3500).animate({ opacity: 1 }, 400);
$("#buttons-draftbot-intro-3").delay(4000).animate({ opacity: 1 }, 400);

$('#myTabs a').click(function (e) {
    e.preventDefault()
    $(this).tab('show')
})