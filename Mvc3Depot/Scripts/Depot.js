function blindDownCart() {
    $('#cart').show('blind', { direction: 'vertical' });
}

function hideNotice() {
    $('#notice').hide();
}

function highlightCurrentItem() {
    $('#current_item').effect('highlight', { color: '#88bbff' }, 1500);
}
