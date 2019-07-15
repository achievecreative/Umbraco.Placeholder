function placeholderSelectorController($scope, $http, umbRequestHelper, editorState) {
    $scope.loading = true;
    $scope.placeholders = [];
    $scope.selectedPlaceholder = $scope.model.value ? $scope.model.value : '';

    var nodeId = editorState.getCurrent().id;
    if(!nodeId){
        nodeId = editorState.getCurrent().parentId;
    }
    $http.post('/umbraco/backoffice/ac/placeholderapi/findplaceholder?nodeId=' + nodeId).then(function(rsp){
        $scope.loading = false;
        $scope.placeholders = rsp.data.Placholders;
    }, function(rsp){
        $scope.loading = false;
    });

    $scope.placeholderChanged = function(){
        $scope.model.value = $scope.selectedPlaceholder;
    }
}

angular.module("umbraco").controller("AC.PropertyEditor.PlaceholderSelectorController", placeholderSelectorController);